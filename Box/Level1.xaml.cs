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

    public partial class Level1 : Ilevel
    {
        private Position _player = new Position(0, 67, 3, 5);
        private Position _barrel1 = new Position(-256, -195, 2, 4);
        private Position _barrel2 = new Position(256, -195, 2, 6);
        private Position _barrel3 = new Position(-256, 317, 4, 4);
        private Position _barrel4 = new Position(256, 317, 4, 6);

        private static CellState[,] _playingFieldInfo = new CellState[7, 11];

        private List<(int, int)> _winCell = new List<(int, int)> { (1, 1), (1, 9), (5, 1), (5, 9)};

        private List<Position> _objectPositions = new List<Position>();

        private int _movesForThreeStars = 38, _movesForTwoStars = 42, _movesForOneStar = 46;


        private void FillingInfoAboutPlayingField()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0 || j == 0 || i == 6 || j == 10) _playingFieldInfo[i, j] = CellState.Border;
                    else _playingFieldInfo[i, j] = CellState.Free;
                }
            }
            _playingFieldInfo[2, 1] = CellState.Border;
            _playingFieldInfo[2, 2] = CellState.Border;
            _playingFieldInfo[3, 2] = CellState.Border;
            _playingFieldInfo[3, 3] = CellState.Border;
            _playingFieldInfo[4, 1] = CellState.Border;
            _playingFieldInfo[4, 2] = CellState.Border;
            _playingFieldInfo[2, 8] = CellState.Border;
            _playingFieldInfo[2, 9] = CellState.Border;
            _playingFieldInfo[3, 7] = CellState.Border; 
            _playingFieldInfo[3, 8] = CellState.Border;
            _playingFieldInfo[4, 8] = CellState.Border;
            _playingFieldInfo[4, 9] = CellState.Border;


            _playingFieldInfo[3, 5] = CellState.Player;
            _playingFieldInfo[2, 4] = CellState.Box;
            _playingFieldInfo[2, 6] = CellState.Box;
            _playingFieldInfo[4, 4] = CellState.Box;
            _playingFieldInfo[4, 6] = CellState.Box;
        }

        public Level1()
        {
            Width = 1408;
            Height = 896;

            _objectPositions.Add(_player);
            _objectPositions.Add(_barrel1);
            _objectPositions.Add(_barrel2);
            _objectPositions.Add(_barrel3);
            _objectPositions.Add(_barrel4);
        }

        public void Load()
        {
            InitializeComponent();
            FillingInfoAboutPlayingField();
        }

        public CellState[,] GetMap()
        {
            return _playingFieldInfo;
        }

        public List<(int, int)> GetWinCell()
        {
            return _winCell;
        }

        public int GetMovesForThreeStars()
        {
            return _movesForThreeStars;
        }

        public int GetMovesForTwoStars()
        {
            return _movesForTwoStars;
        }

        public int GetMovesForOneStar()
        {
            return _movesForOneStar;
        }

        public List<Position> GetObjectsPositions()
        {
            return _objectPositions;
        }
    }
}
