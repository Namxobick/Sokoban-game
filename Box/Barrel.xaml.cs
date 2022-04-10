using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Box
{

    public partial class Barrel : UserControl
    {
        private Transform _transform;
        private MoveBarrel _moveBarrel;
        public Barrel()
        {
            _transform = new Transform();
            _moveBarrel = new MoveBarrel(_transform);
            Width = 128;
            Height = 128;
            InitializeComponent();
            _transform.OnPositionChanged += () =>
            {
                Margin = new Thickness(_transform.Position.X, _transform.Position.Y, 0, 0);
            };
        }

        public void SetPosition(int x, int y, int i, int j)
        {
            _transform.Position = new Point(x, y);
            _transform.PositionI = i;
            _transform.PositionJ = j;
        }
    }
}
