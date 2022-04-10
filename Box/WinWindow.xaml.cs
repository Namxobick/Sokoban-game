using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Box
{
    public partial class EndWindow : UserControl
    {
        public delegate void RestartGame();
        public event RestartGame GameRestart;

        public delegate void SwitchToTheNextLevel();
        public event SwitchToTheNextLevel LevelSwtitch;

        public EndWindow(string level, int movesCount, int movesCountForOneStar, int movesCountForTwoStars, int movesCountForThreeStars)
        {
            InitializeComponent();
            restart.Fill = new ImageBrush(new BitmapImage(new Uri(@"restart.png", UriKind.Relative)));
            nextLevel.Fill = new ImageBrush(new BitmapImage(new Uri(@"next.png", UriKind.Relative)));

            _levelLabel.Content = level;

            _movesLabel.Content = movesCount.ToString();

            restart.MouseLeftButtonDown += RestartMouseLeftButtonDown;
            restart.MouseEnter += RestartMouseEnter;
            restart.MouseLeave += RestartMouseLeave;

            nextLevel.MouseLeftButtonDown += NextLevelMouseLeftButtonDown;
            nextLevel.MouseEnter += NextLevelMouseEnter;
            nextLevel.MouseLeave += NextLevelMouseLeave;

            if (movesCount <= movesCountForThreeStars)
            {
                _star1.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
                _star2.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
                _star3.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
            }
            else if (movesCount <= movesCountForTwoStars)
            {
                _star1.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
                _star2.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
                _star3.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
            }
            else if (movesCount <= movesCountForOneStar)
            {
                _star1.Fill = new ImageBrush(new BitmapImage(new Uri(@"star+.png", UriKind.Relative)));
                _star2.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
                _star3.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
            }
            else
            {
                _star1.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
                _star2.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));
                _star3.Fill = new ImageBrush(new BitmapImage(new Uri(@"star-.png", UriKind.Relative)));

                nextLevel.MouseLeftButtonDown -= NextLevelMouseLeftButtonDown;
                nextLevel.MouseEnter -= NextLevelMouseEnter;
                nextLevel.MouseLeave -= NextLevelMouseLeave;
                BlurEffect meef = new BlurEffect();
                meef.Radius = 10;
                nextLevel.Effect = meef;

            }

        }


        private void RestartMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 0;
            restart.Effect = meef;
        }
        private void RestartMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 5;
            restart.Effect = meef;
        }
        private void RestartMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GameRestart?.Invoke();
        }

        private void NextLevelMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 0;
            nextLevel.Effect = meef;
        }
        private void NextLevelMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 5;
            nextLevel.Effect = meef;
        }
        private void NextLevelMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LevelSwtitch?.Invoke();
        }
    }
}
