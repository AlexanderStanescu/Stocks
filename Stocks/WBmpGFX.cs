using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Drawing;
using System.IO;
using System.Windows;

namespace Stocks
{
    unsafe public class WBmpGFX
    {
        private WriteableBitmap _bmp;
        private int _width;
        private int _height;
        List<Label> TextList;

        public WBmpGFX(int width, int height)
        {
            _width = width;
            _height = height;

            _bmp = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Bgra32, null);
            TextList = new List<Label>();
        }

        public void DrawLineAA(double x1, double y1, double x2, double y2, System.Windows.Media.Color color)
        {
            DrawLineAA((int)x1, (int)y1, (int)x2, (int)y2, color);
        }

        public void DrawLine(double x1, double y1, double x2, double y2, System.Windows.Media.Color color)
        {
            DrawLine((int)x1, (int)y1, (int)x2, (int)y2, color);
        }

        public void DrawLineAA(int x1, int y1, int x2, int y2, System.Windows.Media.Color color)
        {
            _bmp.DrawLineAa(x1, y1, x2, y2, color);
        }

        public void DrawLine(int x1, int y1, int x2, int y2, System.Windows.Media.Color color)
        {
            _bmp.DrawLine(x1, y1, x2, y2, color);
        }

        public void DrawText(double x1, double y1, double x2, double y2, string text)
        {
            DrawText((int)x1, (int)y1, (int)x2, (int)y2, text);
        }

        public WriteableBitmap BMP
        {
            get { return _bmp; }
        }

        public void Clear(System.Windows.Media.Color color)
        {
            _bmp.Clear(color);
        }

        public void AddText(Label label)
        {
            TextList.Add(label);
        }

        public void RenderText()
        {
            RenderTargetBitmap rtBitmap = new RenderTargetBitmap(_width, _height, 96.0, 96.0, PixelFormats.Pbgra32);

            Grid grid = new Grid();
            grid.BeginInit();
            grid.Background = new ImageBrush(_bmp);
            grid.Arrange(new Rect(new System.Windows.Size(_width, _height)));
            
            foreach (Label l in TextList)
            {
                TextBlock txtBlock = new TextBlock();
                txtBlock.BeginInit();
                txtBlock.FontSize = 12;
                txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
                txtBlock.Text = l.Text;
                txtBlock.Arrange(new Rect(l.X1, l.Y1, l.X2 - l.X1, l.Y2 - l.Y1));
                txtBlock.EndInit();

                grid.Children.Add(txtBlock);
            }

            grid.EndInit();

            rtBitmap.Render(grid);

            _bmp.Lock();
            rtBitmap.CopyPixels(new Int32Rect(0, 0, rtBitmap.PixelWidth, rtBitmap.PixelHeight), _bmp.BackBuffer, _bmp.BackBufferStride * _bmp.PixelHeight, _bmp.BackBufferStride);
            _bmp.AddDirtyRect(new Int32Rect(0, 0, _width, _height));
            _bmp.Unlock();

            TextList.Clear();
        }

        public struct Label
        {
            public Label(int x1, int y1, int x2, int y2, string text)
            {
                this.X1 = x1;
                this.Y1 = y1;
                this.X2 = x2;
                this.Y2 = y2;
                this.Text = text;
            }

            public Label(double x1, double y1, double x2, double y2, string text) : this((int)x1, (int)y1, (int)x2, (int)y2, text) { }

            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
            public string Text;
        }
    }
}
