using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Stocks
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : UserControl
    {
        public static readonly DependencyProperty DataProperty;
        WBmpGFX _gfx;       
        
        static Chart()
        {
            FrameworkPropertyMetadata mdData = new FrameworkPropertyMetadata(DataPropertyChanged);
            DataProperty = DependencyProperty.Register("Data", typeof(List<ChartPoint>[]), typeof(Chart), mdData);
        }

        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {
            _gfx = new WBmpGFX((int)this.ActualWidth, (int)this.ActualHeight);

            imgChart.Source = _gfx.BMP;
            imgChart.Stretch = Stretch.Fill;
        }

        private static void DataPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Chart me = (Chart)o;

            if (me._gfx == null)
                me._gfx = new WBmpGFX((int)me.ActualWidth, (int)me.ActualHeight);

            me._gfx.Clear(Colors.Transparent);          

            if (me.Data != null && me.Data.Count() != 0)
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                var data = me.Data.SelectMany(i => i).ToList();

                //General Values
                double width = me.ActualWidth - 51.0;
                double height = me.ActualHeight - 50.0;
                int count = data.Count;

                //X-Axis Calculations

                DateTime minX = data.Min(d => d.Date);
                DateTime maxX = data.Max(d => d.Date);
                int monthDiff = ((maxX.Year - minX.Year) * 12) + (maxX.Month - minX.Month) + 1;
                int interval = 0;
                double deltaAxisX = 0;
                double deltaX = 0;

                //Y-Axis Calculations
                double minY = data.Min(c => c.Close);
                double maxY = data.Max(c => c.Close);
                double yDiff = maxY - minY;
                double ySpan = yDiff / 4.0;
                double[] incrementValues = { 0.125, 0.5, 1.0, 2.0, 5.0, 10.0, 25.0, 50.0, 100.0, 250.0, 500.0, 1000.0, 5000.0, 10000.0, 25000.0, 50000.0, 100000.0 };
                double yIncrement = incrementValues.TakeWhile(p => p <= ySpan).Last();
                double minYAdjusted = Math.Floor(minY / yIncrement) * yIncrement;
                double maxYAdjusted = Math.Ceiling(maxY / yIncrement) * yIncrement;
                double deltaAxisY = 0;
                DateTime beginAxesX = minX;

                if (monthDiff == 1) // BUG: Spanning one month in time where begindate is halfway in one month and enddate is a little before halfway in second results in 2 months, not one
                {
                    //Display Days X Axis
                    interval = (int)(maxX - minX).TotalDays;
                    deltaX = width / interval;
                }
                else if (monthDiff > 24)
                {
                    //Display Years X Axis
                    interval = maxX.Year - minX.Year + 1;
                    beginAxesX = new DateTime(minX.Year, 1, 1);
                    deltaX = width / ((new DateTime(maxX.Year, 12, 31) - beginAxesX).TotalDays - 1); // -1 Because the first coordinate should go on leftmost vertical axes, the last coordinate should be on rightmost vertical axes.
                }
                else
                {
                    //Display Months X Axis
                    interval = monthDiff;
                    beginAxesX = new DateTime(minX.Year, minX.Month, 1);
                    deltaX = width / ((new DateTime(maxX.Year, maxX.Month, DateTime.DaysInMonth(maxX.Year, maxX.Month)) - beginAxesX).TotalDays - 1);
                }

                //Get distance between each vertical bar
                deltaAxisX = width / interval;               

                //Loop through # of vertical bars
                for (int i = 0; i <= interval; i++)
                {
                    //Draw intermittent 4 light green bars between each major dark green bar
                    for (int j = 1; j < 5; j++)
                    {
                        me._gfx.DrawLine(50 + Math.Round(i * deltaAxisX) + Math.Round(j * (deltaAxisX / 5)), 0, 50 + Math.Round(i * deltaAxisX) + Math.Round(j * (deltaAxisX / 5)), height, Colors.LightGreen);
                    }

                    //Draw major dark green bars
                    me._gfx.DrawLine(Math.Round(50 + deltaAxisX * i), 0, Math.Round(50 + deltaAxisX * i), height + 10, Colors.Green);

                    //Draw axes labels
                    if (monthDiff == 1)
                    {
                        me._gfx.AddText(new WBmpGFX.Label(Math.Round(50.0 + deltaAxisX * i), height, Math.Round(50.0 + deltaAxisX * (i + 1)), height + 50.0, minX.AddDays(i + 1).ToString("MMM dd")));
                    }
                    else if (monthDiff > 24)
                    {
                        me._gfx.AddText(new WBmpGFX.Label(Math.Round(50.0 + deltaAxisX * i), height, Math.Round(50.0 + deltaAxisX * (i + 1)), height + 50.0, minX.AddYears(i).ToString("yyyy")));
                    }
                    else
                    {
                        if (i == 0 || minX.AddMonths(i).Month == 1)
                            me._gfx.AddText(new WBmpGFX.Label(Math.Round(50.0 + deltaAxisX * i), height, Math.Round(50.0 + deltaAxisX * (i + 1)), height + 50.0, minX.AddMonths(i).ToString("MMM yyyy")));
                        else
                            me._gfx.AddText(new WBmpGFX.Label(Math.Round(50.0 + deltaAxisX * i), height, Math.Round(50.0 + deltaAxisX * (i + 1)), height + 50.0, minX.AddMonths(i).ToString("MMM")));
                    }
                }

                //Get number of y axis separators
                interval = (int)((maxYAdjusted - minYAdjusted) / yIncrement);

                //Get distance between each y axis separator
                deltaAxisY = height / interval;

                //Loop through each y axis bar
                for (int i = 0; i <= interval; i++)
                {
                    //Draw 4 intermittent light green bars in between
                    for (int j = 1; j < 5; j++)
                    {
                        me._gfx.DrawLine(50, Math.Round(deltaAxisY * (i - 1)) + Math.Round(j * (deltaAxisY / 5)), width + 50, Math.Round(deltaAxisY * (i - 1)) + Math.Round(j * (deltaAxisY / 5)), Colors.LightGreen);
                    }

                    //Draw major dark green bars
                    me._gfx.DrawLine(40, Math.Round(deltaAxisY * i), width + 50, Math.Round(deltaAxisY * i), Colors.Green);

                    //If it's the last label to be drawn, we don't want it half off the screen so adjust the coordinates
                    //Otherwise, just draw the label like before
                    if (i == interval)
                        me._gfx.AddText(new WBmpGFX.Label(0, 0, 50, 20, (minYAdjusted + (i * yIncrement)).ToString()));
                    else
                        me._gfx.AddText(new WBmpGFX.Label(0, height - Math.Round(deltaAxisY * i) - 10, 50, height - Math.Round(deltaAxisY * i) + 10, (minYAdjusted + (i * yIncrement)).ToString()));
                }

                //Finally, render all text at once
                me._gfx.RenderText();

                for (int i = 0; i < count - 1; i++)
                {
                    if (i < count / 5 - 1)
                    {
                        me._gfx.DrawLineAA(50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i].Date - minX).TotalDays), height - Math.Round(height * (data[i].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)),
                                      50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i + 1].Date - minX).TotalDays), height - Math.Round(height * (data[i + 1].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)), Colors.Red);
                    }
                    else if (i < 2 * count / 5 - 1 && i > count / 5 - 1)
                    {
                        me._gfx.DrawLineAA(50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i].Date - minX).TotalDays), height - Math.Round(height * (data[i].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)),
                                         50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i + 1].Date - minX).TotalDays), height - Math.Round(height * (data[i + 1].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)), Colors.Purple);
                    }
                    else if (i < 3 * count / 5 - 1 && i > 2 * count / 5 - 1)
                    {
                        me._gfx.DrawLineAA(50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i].Date - minX).TotalDays), height - Math.Round(height * (data[i].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)),
                                      50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i + 1].Date - minX).TotalDays), height - Math.Round(height * (data[i + 1].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)), Colors.Blue);
                    }
                    else if (i < 4 * count / 5 - 1 && i > 3 * count / 5 - 1)
                    {
                        me._gfx.DrawLineAA(50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i].Date - minX).TotalDays), height - Math.Round(height * (data[i].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)),
                                      50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i + 1].Date - minX).TotalDays), height - Math.Round(height * (data[i + 1].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)), Colors.Orange);
                    }                    
                    else if (i > 4 * count / 5 - 1)
                    {
                        me._gfx.DrawLineAA(50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i].Date - minX).TotalDays), height - Math.Round(height * (data[i].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)),
                                      50 + Math.Round(deltaX * (minX - beginAxesX).TotalDays) + Math.Round(deltaX * (data[i + 1].Date - minX).TotalDays), height - Math.Round(height * (data[i + 1].Close - minYAdjusted) / (maxYAdjusted - minYAdjusted)), Colors.Black);
                    }
                }

                Console.WriteLine("Drawing: {0} ms", sw.ElapsedMilliseconds);
                sw.Stop();
            }       
        }

        public List<ChartPoint>[] Data
        {
            get { return (List<ChartPoint>[])GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public struct ChartPoint
        {
            public ChartPoint(DateTime date, double close)
            {
                this.Date = date;
                this.Close = close;
            }

            public DateTime Date;
            public double Close;
        }
    }
}
