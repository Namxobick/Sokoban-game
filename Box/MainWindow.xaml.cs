using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Box
{
    public partial class MainWindow : Window
    {
        private EndWindow _endWindow;
        private Level0 _level0;
        private Level1 _level1;
        private Player _player;
        private Barrel _barrel1, _barrel2, _barrel3, _barrel4;
        private Rectangle _goToMenu, _restartLevel;
        private Rectangle _rect;
        private Label _movesCounts;
        private MyProgressBar _myProgressBar;

        private int _levelNow = 0;

        public MainWindow()
        {
            InitializeComponent();
            mainGrid.Background = new ImageBrush(new BitmapImage(new Uri(@"C:/Users/Namxobick/source/repos/Box/Ананас.jpg", UriKind.Relative)));

            CreateReactangle(ref _rect, Brushes.Black, 0, 0, this.Width, 60);
            _rect.Fill = Brushes.LightSeaGreen;

            LoadLevel0();

            _movesCounts = new Label();
            _movesCounts.FontFamily = new FontFamily("Georgia");
            _movesCounts.FontSize = 32;
            _movesCounts.Content = "Moves: 0";
            _movesCounts.Margin = new Thickness(0, -3, 0, 0);
            grid.Children.Add(_movesCounts);

            _myProgressBar = new MyProgressBar();
            _myProgressBar.SetSettings(GameMap._movesForOneStar + 1, GameMap._movesForOneStar, GameMap._movesForTwoStars, GameMap._movesForThreeStars);
            _myProgressBar.HorizontalAlignment = HorizontalAlignment.Left;
            _myProgressBar.VerticalAlignment = VerticalAlignment.Top;
            _myProgressBar.Margin = new Thickness(200, 10, 0, 0);
            grid.Children.Add(_myProgressBar);

            CreateReactangle(ref _restartLevel, null, 90, 5, 50, 50, new ImageBrush(new BitmapImage(new Uri(@"C:/Users/Namxobick/source/repos/Box/restart.png", UriKind.Relative))));
            _restartLevel.MouseLeftButtonDown += RestartLevelMouseLeftButtonDown;
            _restartLevel.MouseEnter += RestartLevelMouseEnter;
            _restartLevel.MouseLeave += RestartLevelMouseLeave;

            CreateReactangle(ref _goToMenu, null, 15, 5, 50, 50, new ImageBrush(new BitmapImage(new Uri(@"C:/Users/Namxobick/source/repos/Box/menu.png", UriKind.Relative))));
            _goToMenu.MouseEnter += GoToMenuMouseEnter;
            _goToMenu.MouseLeave += GoToMenuMouseLeave;

            GameMap.GameWin += GameEnd;
            GameMap.OnMovesCountsChanged += DrawMovesCountsAndProgressBar;
        }

        private void DrawMovesCountsAndProgressBar(int message)
        {
            _movesCounts.Content = "Moves: " + message.ToString();
            _myProgressBar.ChangeValue(message);
        }

        private void GameEnd()
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 10;
            grid.Effect = meef;
            switch (_levelNow)
            {
                case 0:
                    _endWindow = new EndWindow(_levelNow.ToString(), GameMap.MovesCount, _level0.GetMovesForOneStar(), _level0.GetMovesForTwoStars(), _level0.GetMovesForThreeStars());
                    break;

                case 1:
                    _endWindow = new EndWindow(_levelNow.ToString(), GameMap.MovesCount, _level1.GetMovesForOneStar(), _level1.GetMovesForTwoStars(), _level1.GetMovesForThreeStars());
                    break;

                default:
                    break;
            }
            mainGrid.Children.Add(_endWindow);


            _endWindow.HorizontalAlignment = HorizontalAlignment.Center;
            _endWindow.VerticalAlignment = VerticalAlignment.Center;

            _endWindow.GameRestart += EndWindowRestartGame;
            _endWindow.LevelSwtitch += EndWindowLevelSwtitch; ;
        }

        private void EndWindowLevelSwtitch()
        {
            _levelNow++;
            Restart();
        }

        private void EndWindowRestartGame()
        {
            Restart();
        }

        private void LoadLevel0()
        {
            _level0 = new Level0();
            _barrel1 = new Barrel();
            _barrel2 = new Barrel();
            _player = new Player();

            _level0.OnInitPlayer += _player.SetPosition;
            _level0.OnInitBarrel1 += _barrel1.SetPosition;
            _level0.OnInitBarrel2 += _barrel2.SetPosition;

            _level0.Margin = new Thickness(0, 60, 0, 0);
            _level0.Load();
            grid.Children.Add(_level0);
            grid.Children.Add(_player);
            grid.Children.Add(_barrel1);
            grid.Children.Add(_barrel2);


            GameMap.SetMap(_level0.GetMap());
            GameMap.SetWinCell(_level0.GetWinCell());
            GameMap.SetMovesForStar(_level0.GetMovesForOneStar(), _level0.GetMovesForTwoStars(), _level0.GetMovesForThreeStars());

            this.KeyDown += InputSystem.PlayerKeyDown;

            this.Width = _level0.Width + 15;
            this.Height = _level0.Height + 90;
        }

        private void LoadLevel1()
        {
            _level1 = new Level1();
            _barrel1 = new Barrel();
            _barrel2 = new Barrel();
            _barrel3 = new Barrel();
            _barrel4 = new Barrel();
            _player = new Player();

            _level1.OnInitPlayer += _player.SetPosition;

            _level1.OnInitBarrel1 += _barrel1.SetPosition;
            _level1.OnInitBarrel2 += _barrel2.SetPosition;
            _level1.OnInitBarrel3 += _barrel3.SetPosition;
            _level1.OnInitBarrel4 += _barrel4.SetPosition;

            _level1.Margin = new Thickness(0, 60, 0, 0);
            _level1.Load();
            grid.Children.Add(_level1);
            grid.Children.Add(_player);

            grid.Children.Add(_barrel1);
            grid.Children.Add(_barrel2);
            grid.Children.Add(_barrel3);
            grid.Children.Add(_barrel4);


            GameMap.SetMap(_level1.GetMap());
            GameMap.SetWinCell(_level1.GetWinCell());
            GameMap.SetMovesForStar(_level1.GetMovesForOneStar(), _level1.GetMovesForTwoStars(), _level1.GetMovesForThreeStars());

            this.KeyDown += InputSystem.PlayerKeyDown;

            this.Width = _level1.Width + 15;
            this.Height = _level1.Height + 99;
        }

        private void RestartLevelMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 0;
            _restartLevel.Effect = meef;
            
        }
        private void RestartLevelMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 5;
            _restartLevel.Effect = meef;
        }
        private void RestartLevelMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Restart();
        }

        private void GoToMenuMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 5;
            _goToMenu.Effect = meef;
        }
        private void GoToMenuMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BlurEffect meef = new BlurEffect();
            meef.Radius = 0;
            _goToMenu.Effect = meef;
        }

        private void CreateReactangle(ref Rectangle rect, Brush stroke, double marginRight, double marginUp, double width = 50, double height = 50, ImageBrush image = null)
        {
            rect = new Rectangle();
            rect.Margin = new Thickness(0, marginUp, marginRight, 0);
            rect.HorizontalAlignment = HorizontalAlignment.Right;
            rect.VerticalAlignment = VerticalAlignment.Top;
            rect.Stroke = stroke;
            rect.Width = width;
            rect.Height = height;
            rect.Fill = image;
            grid.Children.Add(rect);
        }

        private void Restart()
        {
            this.KeyDown -= InputSystem.PlayerKeyDown;
            InputSystem.Clear();
            mainGrid.Children.Remove(_endWindow);

            BlurEffect meef = new BlurEffect();
            meef.Radius = 0;
            grid.Effect = meef;

            grid.Children.Clear();
            grid.Children.Add(_rect);
            grid.Children.Add(_movesCounts);
            grid.Children.Add(_myProgressBar);
            grid.Children.Add(_goToMenu);
            grid.Children.Add(_restartLevel);         

            switch (_levelNow)
            {
                case 0:
                    LoadLevel0();
                    break;

                case 1:
                    LoadLevel1();
                    break;

                default:
                    break;
            }

            _movesCounts.Content = "Moves: 0";
            _myProgressBar.ChangeValue(0);
            _myProgressBar.SetSettings(GameMap._movesForOneStar + 1, GameMap._movesForOneStar, GameMap._movesForTwoStars, GameMap._movesForThreeStars);
        }
    }
}
