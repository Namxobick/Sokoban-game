using System.Windows;

namespace Box
{
    public class MoveBarrel
    {
        private Transform _transform;

        public MoveBarrel(Transform transform)
        {
            _transform = transform;

            InputSystem.Up += () =>
            {

                if (GameMap.InfoCell(_transform.PositionI - 1, _transform.PositionJ) == CellState.Border ||
                    GameMap.InfoCell(_transform.PositionI - 1, _transform.PositionJ) == CellState.Box)
                    return;

                else if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ) == CellState.Player)
                {
                    GameMap.UpdateBarrelCell(_transform.PositionI, transform.PositionJ, _transform.PositionI - 1, transform.PositionJ);

                    _transform.PositionI--;
                    MotionAnimation motionAnimation = new MotionAnimation(_transform, 0, -256);
                    
                    //_transform.Position = new Point(_transform.Position.X, _transform.Position.Y - 256);
                }
            };

            InputSystem.Down += () =>
            {

                if (GameMap.InfoCell(_transform.PositionI + 1, _transform.PositionJ) == CellState.Border ||
                    GameMap.InfoCell(_transform.PositionI + 1, _transform.PositionJ) == CellState.Box)
                    return;

                else if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ) == CellState.Player)
                {
                    GameMap.UpdateBarrelCell(_transform.PositionI, transform.PositionJ, _transform.PositionI + 1, transform.PositionJ);

                    _transform.PositionI++;
                    MotionAnimation motionAnimation = new MotionAnimation(_transform, 0, 256);
                    //_transform.Position = new Point(_transform.Position.X, _transform.Position.Y + 256);
                }
            };

            InputSystem.Left += () =>
            {

                if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ - 1) == CellState.Border ||
                    GameMap.InfoCell(_transform.PositionI, _transform.PositionJ - 1) == CellState.Box)
                    return;

                else if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ) == CellState.Player)
                {
                    GameMap.UpdateBarrelCell(_transform.PositionI, transform.PositionJ, _transform.PositionI, transform.PositionJ - 1);

                    _transform.PositionJ--;
                    MotionAnimation motionAnimation = new MotionAnimation(_transform, -256, 0);
                    //_transform.Position = new Point(_transform.Position.X - 256, _transform.Position.Y);
                }
            };

            InputSystem.Right += () =>
            {

                if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ + 1) == CellState.Border ||
                    GameMap.InfoCell(_transform.PositionI, _transform.PositionJ + 1) == CellState.Box)
                    return;

                else if (GameMap.InfoCell(_transform.PositionI, _transform.PositionJ) == CellState.Player)
                {
                    GameMap.UpdateBarrelCell(_transform.PositionI, transform.PositionJ, _transform.PositionI, transform.PositionJ + 1);

                    _transform.PositionJ++;
                    MotionAnimation motionAnimation = new MotionAnimation(_transform, 256, 0);
                    //_transform.Position = new Point(_transform.Position.X + 256, _transform.Position.Y);
                }
            };
        }
    }
}
