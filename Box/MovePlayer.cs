using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Box
{
    public class MovePlayer
    {
        public Transform _transform;

        public MovePlayer(Transform transform)
        {

            _transform = transform;

            InputSystem.Up += () =>
            {
                _transform.TargetDirection = Transform.Direction.Up;

                if (GameMap.InfoCell(_transform.PositionI - 1, _transform.PositionJ) == CellState.Border)
                    return;

                if (GameMap.InfoCell(_transform.PositionI - 1, _transform.PositionJ) == CellState.Box)
                    if (GameMap.InfoCell(_transform.PositionI - 2, _transform.PositionJ) == CellState.Border ||
                        GameMap.InfoCell(_transform.PositionI - 2, _transform.PositionJ) == CellState.Box)
                        return;

                GameMap.UpdatePlayerCell(_transform.PositionI, transform.PositionJ, _transform.PositionI - 1, transform.PositionJ);

                _transform.PositionI--;

                MotionAnimation motionAnimation = new MotionAnimation(_transform, 0, -256);
                //_transform.Position = new Point(_transform.Position.X, _transform.Position.Y - 256);
            };

            InputSystem.Down += () =>
            {
                _transform.TargetDirection = Transform.Direction.Down;

                if (GameMap.InfoCell(_transform.PositionI + 1, _transform.PositionJ) == CellState.Border)
                    return;

                if (GameMap.InfoCell(_transform.PositionI + 1, _transform.PositionJ) == CellState.Box)
                    if (GameMap.InfoCell(_transform.PositionI + 2, _transform.PositionJ) == CellState.Border ||
                        GameMap.InfoCell(_transform.PositionI + 2, _transform.PositionJ) == CellState.Box)
                        return;

                GameMap.UpdatePlayerCell(_transform.PositionI, transform.PositionJ, _transform.PositionI + 1, transform.PositionJ);

                _transform.PositionI++;

                MotionAnimation motionAnimation = new MotionAnimation(_transform, 0, 256);
                //_transform.Position = new Point(_transform.Position.X, _transform.Position.Y + 256);
            };

            InputSystem.Left += () =>
            {
                _transform.TargetDirection = Transform.Direction.Left;

                if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ - 1) == CellState.Border)
                    return;

                if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ - 1) == CellState.Box)
                    if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ - 2) == CellState.Border ||
                        GameMap.InfoCell(_transform.PositionI, _transform.PositionJ - 2) == CellState.Box)
                        return;

                GameMap.UpdatePlayerCell(_transform.PositionI, transform.PositionJ, _transform.PositionI, transform.PositionJ - 1);

                _transform.PositionJ--;

                MotionAnimation motionAnimation = new MotionAnimation(_transform, -256, 0);
                //_transform.Position = new Point(_transform.Position.X - 256, _transform.Position.Y);
            };

            InputSystem.Right += () =>
            {
                _transform.TargetDirection = Transform.Direction.Right;

                if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ + 1) == CellState.Border)
                    return;

                if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ + 1) == CellState.Box)
                    if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ + 2) == CellState.Border ||
                        GameMap.InfoCell(_transform.PositionI, _transform.PositionJ + 2) == CellState.Box)
                        return;

                GameMap.UpdatePlayerCell(_transform.PositionI, transform.PositionJ, _transform.PositionI, transform.PositionJ + 1);

                _transform.PositionJ++;

                MotionAnimation motionAnimation = new MotionAnimation(_transform, 256, 0);
                //_transform.Position = new Point(_transform.Position.X + 256, _transform.Position.Y);
            };
        }
    }
}



