using System;
using System.Windows;

namespace Box
{
    public class Transform
    {
        public enum Direction
        {
            Up, Down, Left, Right
        };

        private Point _position;
        private int _positionI, _positionJ;
        private Direction _direction;

        public Point Position {
            get 
            {
                return _position;
            } 
            set
            {
                _position = value;
                OnPositionChanged?.Invoke();
            }
        }

        public Direction TargetDirection
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
                OnDirectionChanged?.Invoke();
            }
        }

        public int PositionI
        {
            get
            {
                return _positionI;
            }

            set
            {
                _positionI = value;
            }
        }

        public int PositionJ
        {
            get
            {
                return _positionJ;
            }

            set
            {
                _positionJ = value;
            }
        }

        public Action OnPositionChanged;

        public Action OnDirectionChanged;

        public Transform()
        {
            _position = new Point(0, 0);
            _direction = Direction.Right;
        }

        public Transform(Point position, Direction direction = Direction.Right)
        {
            _position = position;
            _direction = direction;
        }

    }
}
