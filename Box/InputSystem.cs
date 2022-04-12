using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Box
{
    static class InputSystem
    {
        public static Action Up, Down, Left, Right;
        public static void PlayerKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    Up?.Invoke(); 
                    break;
                case Key.Down:
                case Key.S:
                    Down?.Invoke();
                    break;
                case Key.Left:
                case Key.A:
                    Left?.Invoke();
                    break;
                case Key.Right:
                case Key.D:
                    Right?.Invoke();
                    break;

                default:
                    break;
            }
        }

        public static void Clear()
        {
            Up = null;
            Down = null;
            Left = null;
            Right = null;
        }
    }
}
