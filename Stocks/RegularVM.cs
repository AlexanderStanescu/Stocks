using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;

namespace Stocks
{
    public class RegularVM : BaseVM
    {

        public RegularVM() 
        {
            this.SearchText = "";
            this.BeginDate = "";
            this.EndDate = "";

            DataGrabber.Connect();
        }

        private void UpdateStock()
        {
            try
            {
                if (_searchText == "")
                {
                    this.ChartPoints = null;
                }
                else
                {
                    DataTable[] dt = new DataTable[5];
                    string beginDate = this.BeginDate;
                    string endDate = this.EndDate;

                    if (this.BeginDate == null || this.EndDate == null || this.BeginDate.CompareTo("") == 0 || this.EndDate.CompareTo("") == 0)
                    {
                        beginDate = DateTime.Today.Subtract(new TimeSpan(365, 0, 0, 0)).ToString();
                        endDate = DateTime.Today.ToString();
                    }

                    dt[0] = DataGrabber.GetStockCloseInfo(_searchText, beginDate, endDate);

                    List<Chart.ChartPoint>[] pts = new List<Chart.ChartPoint>[5];
                    pts[0] = new List<Chart.ChartPoint>();
                    pts[1] = new List<Chart.ChartPoint>();
                    pts[2] = new List<Chart.ChartPoint>();
                    pts[3] = new List<Chart.ChartPoint>();
                    pts[4] = new List<Chart.ChartPoint>();

                    Parallel.For(0, 5, i =>
                    {
                        if (i == 1)
                            dt[1] = DataGrabber.GetStockConversionLine(_searchText, beginDate, endDate, 9);
                        else if (i == 2)
                            dt[2] = DataGrabber.GetStockConversionLine(_searchText, beginDate, endDate, 26);
                        else if (i == 3)
                            dt[3] = DataGrabber.GetStockLeadingSpanALine(_searchText, beginDate, endDate, 9, 26);  
                        else if (i == 4)
                            dt[4] = DataGrabber.GetStockConversionLine(_searchText, beginDate, endDate, 52);  

                        foreach (DataRow row in dt[i].Rows)
                        {
                            pts[i].Add(new Chart.ChartPoint((DateTime)row[0], Math.Round((double)row[1], 2)));
                        }
                    });

                    this.ChartPoints = pts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                UpdateStock();
            }
        }

        private string _beginDate;
        public string BeginDate
        {
            get { return _beginDate; }
            set
            {
                _beginDate = value;
                OnPropertyChanged("BeginDate");
                UpdateStock();
            }
        }

        private string _endDate;
        public string EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
                UpdateStock();
            }
        }

        private string _stockName;
        public string StockName
        {
            get { return _stockName; }
            set
            {
                _stockName = value;
                OnPropertyChanged("StockName");
            }
        }

        private DateTime _stockDate;
        public DateTime StockDate
        {
            get { return _stockDate; }
            set
            {
                _stockDate = value;
                OnPropertyChanged("StockDate");
            }
        }

        private double _stockLow;
        public double StockLow
        {
            get { return _stockLow; }
            set
            {
                _stockLow = value;
                OnPropertyChanged("StockLow");
            }
        }

        private double _stockHigh;
        public double StockHigh
        {
            get { return _stockHigh; }
            set
            {
                _stockHigh = value;
                OnPropertyChanged("StockHigh");
            }
        }

        private double _stockOpen;
        public double StockOpen
        {
            get { return _stockOpen; }
            set
            {
                _stockOpen = value;
                OnPropertyChanged("StockOpen");
            }
        }

        private double _stockClose;
        public double StockClose
        {
            get { return _stockClose; }
            set
            {
                _stockClose = value;
                OnPropertyChanged("StockClose");
            }
        }

        private int _stockVolume;
        public int StockVolume
        {
            get { return _stockVolume; }
            set
            {
                _stockVolume = value;
                OnPropertyChanged("StockVolume");
            }
        }

        private double _stockAdjClose;
        public double StockAdjClose
        {
            get { return _stockAdjClose; }
            set
            {
                _stockAdjClose = value;
                OnPropertyChanged("StockAdjClose");
            }
        }

        private List<Chart.ChartPoint>[] _chartPoints;
        public List<Chart.ChartPoint>[] ChartPoints
        {
            get { return _chartPoints; }
            set
            {
                _chartPoints = value;
                OnPropertyChanged("ChartPoints");
            }
        }
    }
}
