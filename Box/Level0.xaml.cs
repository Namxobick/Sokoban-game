using System;
using System.Collections.Generic;

namespace Box
{
    public enum CellState
    {
        Border, Free, Box, Player, End
    } 

    public struct Position
    {
        public int VisualMarginLeft, VisualMarginRight;
        public int PositionIOnMap, PositionJOnMap;

        public Position(int visualMarginLeft, int visualMarginRight, int positionIOnMap, int positionJOnMap)
        {
            this.VisualMarginLeft = visualMarginLeft;
            this.VisualMarginRight = visualMarginRight;
            this.PositionIOnMap = positionIOnMap;
            this.PositionJOnMap = positionJOnMap;
        }
    }

    public interface Ilevel
    {
        void Load();

        CellState[,] GetMap();

        List<(int, int)> GetWinCell();

        int GetMovesForThreeStars();

        int GetMovesForTwoStars();

        int GetMovesForOneStar();

        List<Position> GetObjectsPositions();
    }

    public partial class Level0 : Ilevel
    {
        private Position _player = new Position(-640, 58, 2, 2);
        private Position _barrel1 = new Position(-128, 58, 2, 4);
        private Position _barrel2 = new Position(128, -198, 1, 5);

        private static CellState[,] _playingFieldInfo = new CellState[5, 10];

        private List<(int, int)> _winCell = new List<(int, int)> {(1, 1), (2, 7) };

        private List<Position> _objectPositions = new List<Position>();

        private int _movesForThreeStars = 9, _movesForTwoStars = 10, _movesForOneStar = 11;


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

            _objectPositions.Add(_player);
            _objectPositions.Add(_barrel1);
            _objectPositions.Add(_barrel2);
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
