using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Box
{
    public partial class Player : UserControl
    {
        private Transform _transform;
        private MovePlayer _movePlayer;

        public Player()
        {
            _transform = new Transform();
            _movePlayer = new MovePlayer(_transform);
            Background = new ImageBrush(new BitmapImage(new Uri(@"1.png", UriKind.Relative)));
            Width = 128;
            Height = 128;
            InitializeComponent();
            _transform.OnPositionChanged += () =>
            {
               Margin = new Thickness(_transform.Position.X, _transform.Position.Y, 0, 0);
            };

            _transform.OnDirectionChanged += () =>
            {
                switch (_transform.TargetDirection)
                {
                    case Transform.Direction.Up:
                        Background = new ImageBrush(new BitmapImage(new Uri(@"4.png", UriKind.Relative)));
                        break;
                    case Transform.Direction.Down:
                        Background = new ImageBrush(new BitmapImage(new Uri(@"3.png", UriKind.Relative)));
                        break;
                    case Transform.Direction.Left:
                        Background = new ImageBrush(new BitmapImage(new Uri(@"2.png", UriKind.Relative)));
                        break;
                    case Transform.Direction.Right:
                        Background = new ImageBrush(new BitmapImage(new Uri(@"1.png", UriKind.Relative)));
                        break;
                    default:
                        break;
                }

            };
        }

        public void SetPosition(int x, int y, int i, int j)
        {
            _transform.Position = new Point(x, y);
            _transform.PositionI = i;
            _transform.PositionJ = j;
        }

    }
}
