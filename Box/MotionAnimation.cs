using System.Threading.Tasks;
using System.Windows;

namespace Box
{
    class MotionAnimation
    {
        Transform _transform;
        double _distanceX, _distanceY;
        int _numberOfFrames;
        public MotionAnimation(Transform transform, int distanceX, int distanceY, int interval = 5, int numberOfFrames = 15)
        {
            _transform = transform;
            _numberOfFrames = numberOfFrames;
            _distanceX = distanceX / (double)numberOfFrames;
            _distanceY = distanceY / (double)numberOfFrames;

            var task = Animate(interval);
        }

        private async Task Animate(int interval)
        {
            while (_numberOfFrames != 0)
            {
                _transform.Position = new Point(_transform.Position.X + _distanceX, _transform.Position.Y + _distanceY);
                _numberOfFrames--;
                await Task.Delay(interval);
            }
        }

    }

}
