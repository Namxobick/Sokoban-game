using System;
using System.Collections.Generic;
using System.Windows;

namespace Box
{
    static class GameMap
    {   
        private static CellState[,] _map;
        private static List<(int, int)> _winCell;

        private static bool _gameEnd;

        public static int _movesForThreeStars = 0, _movesForTwoStars = 0, _movesForOneStar = 0;

        public static int MovesCount = 0;

        public static Action GameEnd;

        public delegate void ChangeMovesCountsHandler(int message);
        public static event ChangeMovesCountsHandler OnMovesCountsChanged;

        public static void SetMap(CellState[,] map)
        {
            _map = map;
            MovesCount = 0;
            _gameEnd = false;
        }
            
        public static void SetWinCell(List<(int, int)> winCell)
        {
            _winCell = winCell;
        }

        public static void SetMovesForStar(int star1, int star2, int star3)
        {
            _movesForThreeStars = star3;
            _movesForTwoStars = star2;
            _movesForOneStar = star1;
        }

        public static void UpdatePlayerCell(int oldI, int oldJ, int newI, int newJ)
        {
            _map[oldI, oldJ] = CellState.Free;
            _map[newI, newJ] = CellState.Player;
            MovesCount++;
            OnMovesCountsChanged.Invoke(MovesCount);

            ChekingOnLoseGame();
        }


        public static void UpdateBarrelCell(int oldI, int oldJ, int newI, int newJ)
        {
            _map[oldI, oldJ] = CellState.Free;
            _map[newI, newJ] = CellState.Box;
            ChekingOnWinGame();
        }

        public static CellState InfoCell(int i, int j)
        {
            return _map[i, j];
        }

        private static void ChekingOnLoseGame()
        {
            if (MovesCount > _movesForOneStar && !_gameEnd)
            {
                InputSystem.Clear();
                GameEnd?.Invoke();
                _gameEnd = true;
            }
        }

        public static void ChekingOnWinGame()
        {
            bool isGameWin = true;

            foreach (var item in _winCell)
            {
                if (_map[item.Item1, item.Item2] != CellState.Box)
                {
                    isGameWin = false;
                    return;
                }
            }
            if (isGameWin && !_gameEnd)
            {
                InputSystem.Clear();
                GameEnd?.Invoke();
                _gameEnd = true;
            }
        }
    }
}
