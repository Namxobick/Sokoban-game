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

    public partial class Level1 : UserControl
    {
        private static CellState[,] _playingFieldInfo = new CellState[7, 11];

        private List<(int, int)> _winCell = new List<(int, int)> { (1, 1), (1, 9), (5, 1), (5, 9)};

        private int _movesForThreeStars = 38, _movesForTwoStars = 42, _movesForOneStar = 46;

        public Action<int, int, int, int> OnInitPlayer;
        public Action<int, int, int, int> OnInitBarrel1;
        public Action<int, int, int, int> OnInitBarrel2;
        public Action<int, int, int, int> OnInitBarrel3;
        public Action<int, int, int, int> OnInitBarrel4;

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
        }

        public void Load()
        {
            InitializeComponent();
            FillingInfoAboutPlayingField();

            OnInitPlayer?.Invoke(0, 67, 3, 5);

            OnInitBarrel1?.Invoke(-256, -195, 2, 4);
            OnInitBarrel2?.Invoke(256, -195, 2, 6);
            OnInitBarrel3?.Invoke(-256, 317, 4, 4);
            OnInitBarrel4?.Invoke(256, 317, 4, 6);
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
    }
}
