using System;
using System.Collections.Generic;

namespace Box
{
    public enum CellState
    {
        Border, Free, Box, Player, End
    } 


    public partial class Level0
    {
        private static CellState[,] _playingFieldInfo = new CellState[5, 10];

        private List<(int, int)> _winCell =new List<(int, int)> {(1, 1), (2, 7) };

        private int _movesForThreeStars = 9, _movesForTwoStars = 10, _movesForOneStar = 11;

        public Action<int, int, int, int> OnInitPlayer;
        public Action<int, int, int, int> OnInitBarrel1;
        public Action<int, int, int, int> OnInitBarrel2;

        private void FillingInfoAboutPlayingField()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || j == 0 || i == 4 || j == 9) _playingFieldInfo[i, j] = CellState.Border;
                    else _playingFieldInfo[i, j] = CellState.Free;
                }
            }
            _playingFieldInfo[2, 2] = CellState.Player;
            _playingFieldInfo[2, 4] = CellState.Box;
            _playingFieldInfo[1, 5] = CellState.Box;
        }

        public Level0()
        {
            Width = 1280;
            Height = 640; 
        }

        public void Load()
        {
            InitializeComponent();
            FillingInfoAboutPlayingField();
            OnInitPlayer?.Invoke(-640, 67, 2, 2);
            OnInitBarrel1?.Invoke(-128, 67, 2, 4);
            OnInitBarrel2?.Invoke(128, -189, 1, 5);
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
