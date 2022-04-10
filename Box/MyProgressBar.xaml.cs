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

namespace Box
{
    public partial class MyProgressBar : UserControl
    {
        private const int width = 200, height = 50;

        GradientStopCollection _gsc;

        private double _step;
        private int _maxValue, _value, _valueStar1, _valueStar2, _valueStar3;

        public MyProgressBar()
        {
            InitializeComponent();
            Width = width;
            Height = height;

            _gsc = new GradientStopCollection();

            _gsc.Add(new GradientStop()
            {
                Color = Color.FromRgb(161, 176, 6),
                Offset = 1.0
            });

            _gsc.Add(new GradientStop()
            {
                Color = Color.FromRgb(73, 73, 73),
                Offset = 1.0
            });

            progressBar.Fill = new LinearGradientBrush(_gsc, 0)
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0)
            };

        }

        public void SetSettings(int maxValue, int valueStar1, int valueStar2, int valueStar3)
        {
            _maxValue = maxValue;
            _valueStar1 = valueStar1;
            _valueStar2 = valueStar2;
            _valueStar3 = valueStar3;
            _step = width / (double)(maxValue);

            star1.Margin = new Thickness(_step * (maxValue - valueStar1) - 12, 25, 0, 0);
            star2.Margin = new Thickness(_step * (maxValue - valueStar2) - 12, 25, 0, 0);
            star3.Margin = new Thickness(_step * (maxValue - valueStar3) - 12, 25, 0, 0);

            star3.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
            star2.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
            star1.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));

            line1.Margin = new Thickness(_step * (maxValue - valueStar1), 0, 0, 0);
            line2.Margin = new Thickness(_step * (maxValue - valueStar2), 0, 0, 0);
            line3.Margin = new Thickness(_step * (maxValue - valueStar3), 0, 0, 0);
        }

        public void ChangeValue(int value)
        {
            _gsc.Clear();
            double offset = (_maxValue - value) / (double)(_maxValue);

            _gsc.Add(new GradientStop()
            {
                Color = Color.FromRgb(161, 176, 6),
                Offset = offset
            });

            _gsc.Add(new GradientStop()
            {
                Color = Color.FromRgb(73, 73, 73),
                Offset = offset
            });

            progressBar.Fill = new LinearGradientBrush(_gsc, 0)
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0)
            };

            if (value > _valueStar3)
                star3.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
            if (value > _valueStar2)
                star2.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
            if (value > _valueStar1)
                star1.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
        }
    }
}