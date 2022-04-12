using System;
using System.Collections.Generic;
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

        private List<Ilevel> _levels;
        private Level0 _level0;
        private Level1 _level1;

        private Player _player;
        private Rectangle _goToMenu, _restartLevel;
        private Rectangle _rect;
        private Label _movesCounts;
        private MyProgressBar _myProgressBar;

        private int _levelNow = 0;

        public MainWindow()
        {
            InitializeComponent();

            _levels = new List<Ilevel>();
            _level0 = new Level0();
            _level1 = new Level1();

            _levels.Add(_level0);
            _levels.Add(_level1);

            mainGrid.Background = new ImageBrush(new BitmapImage(new Uri(@"Ананас.jpg", UriKind.Relative)));

            CreateReactangle(ref _rect, Brushes.Black, 0, 0, this.Width, 60);
            _rect.Fill = Brushes.LightSeaGreen;

            LoadLevel();

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

            CreateReactangle(ref _restartLevel, null, 90, 5, 50, 50, new ImageBrush(new BitmapImage(new Uri(@"restart.png", UriKind.Relative))));
            _restartLevel.MouseLeftButtonDown += RestartLevelMouseLeftButtonDown;
            _restartLevel.MouseEnter += RestartLevelMouseEnter;
            _restartLevel.MouseLeave += RestartLevelMouseLeave;

            CreateReactangle(ref _goToMenu, null, 15, 5, 50, 50, new ImageBrush(new BitmapImage(new Uri(@"menu.png", UriKind.Relative))));
            _goToMenu.MouseEnter += GoToMenuMouseEnter;
            _goToMenu.MouseLeave += GoToMenuMouseLeave;

            GameMap.GameEnd += GameEnd;
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

            _endWindow = new EndWindow(_levelNow.ToString(), GameMap.MovesCount, _levels[_levelNow].GetMovesForOneStar(), _levels[_levelNow].GetMovesForTwoStars(), _levels[_levelNow].GetMovesForThreeStars());

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

        private void LoadLevel()
        {
            _player = new Player();

            if (_levels.Count <= _levelNow)
            {
                Console.WriteLine("Only two levels");
                _levelNow = 0;
            }

            List<Position> _objectPositions = _levels[_levelNow].GetObjectsPositions();
            List<Barrel> barrels = new List<Barrel>();

            _levels[_levelNow].Load();
            ((UserControl)_levels[_levelNow]).Margin = new Thickness(0, 60, 0, 0);
            grid.Children.Add((UserControl)_levels[_levelNow]);

            for (int i = 1; i < _objectPositions.Count; i++)
            {
                barrels.Add(new Barrel());
                barrels[i - 1].SetPosition(_objectPositions[i].VisualMarginLeft, _objectPositions[i].VisualMarginRight, _objectPositions[i].PositionIOnMap, _objectPositions[i].PositionJOnMap);
                grid.Children.Add(barrels[i - 1]);
            }

            _player.SetPosition(_objectPositions[0].VisualMarginLeft, _objectPositions[0].VisualMarginRight, _objectPositions[0].PositionIOnMap, _objectPositions[0].PositionJOnMap);
            grid.Children.Add(_player);

            GameMap.SetMap(_levels[_levelNow].GetMap());
            GameMap.SetWinCell(_levels[_levelNow].GetWinCell());
            GameMap.SetMovesForStar(_levels[_levelNow].GetMovesForOneStar(), _levels[_levelNow].GetMovesForTwoStars(), _levels[_levelNow].GetMovesForThreeStars());

            this.KeyDown += InputSystem.PlayerKeyDown;

            this.Width = ((UserControl)_levels[_levelNow]).Width + 15;
            this.Height = ((UserControl)_levels[_levelNow]).Height + 99;

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

            LoadLevel();

            _movesCounts.Content = "Moves: 0";
            _myProgressBar.ChangeValue(0);
            _myProgressBar.SetSettings(GameMap._movesForOneStar + 1, GameMap._movesForOneStar, GameMap._movesForTwoStars, GameMap._movesForThreeStars);
        }
    }
}
