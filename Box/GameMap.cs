using System;
using System.Collections.Generic;
using System.Windows;

namespace Box
{
    static class GameMap
    {   
        private static CellState[,] _map;
        private static List<(int, int)> _winCell;

        public static int _movesForThreeStars = 0, _movesForTwoStars = 0, _movesForOneStar = 0;

        public static int MovesCount = 0;

        public static Action GameWin;

        public delegate void ChangeMovesCountsHandler(int message);
        public static event ChangeMovesCountsHandler OnMovesCountsChanged;

        public static void SetMap(CellState[,] map)
        {
            _map = map;
            MovesCount = 0;   
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
            ChekingEndGame();
        }

        public static void UpdateBarrelCell(int oldI, int oldJ, int newI, int newJ)
        {
            _map[oldI, oldJ] = CellState.Free;
            _map[newI, newJ] = CellState.Box;             
        }

        public static CellState InfoCell(int i, int j)
        {
            return _map[i, j];
        }

        public static void ChekingEndGame()
        {
            bool gameEnd = false;

            if (MovesCount > _movesForOneStar)
            {
                InputSystem.Clear();
                GameWin?.Invoke();
                gameEnd = true;
            }

            bool isGameWin = true;

            foreach (var item in _winCell)
            {
                if (_map[item.Item1, item.Item2] != CellState.Box)
                {
                    isGameWin = false;
                    return;
                }
            }
            if (isGameWin && !gameEnd)
            {
                InputSystem.Clear();
                GameWin?.Invoke();
            }
        }
    }
}
