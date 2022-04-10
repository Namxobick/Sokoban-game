using System;
using System.Windows;
using System.Windows.Threading;

namespace Box
{
    class MotionAnimation
    {
        DispatcherTimer _timer;
        Transform _transform;
        int _distanceX, _distanceY, _numberOfFrames;
        public MotionAnimation(Transform transform, int distanceX, int distanceY, int interval = 15, int numberOfFrames = 8)
        {
            _transform = transform;
            _numberOfFrames = numberOfFrames;
            _distanceX = distanceX / numberOfFrames;

            _distanceY = distanceY / numberOfFrames;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(interval);
            _timer.Tick += TimerTick;
            _timer.Start();

        }

        private void TimerTick(object sender, EventArgs e)
        {
            _transform.Position = new Point(_transform.Position.X + _distanceX, _transform.Position.Y + _distanceY);
            _numberOfFrames--;
            if (_numberOfFrames == 0)
                _timer.Stop();
        }
    }

}
