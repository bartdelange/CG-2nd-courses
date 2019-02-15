﻿using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    public class AxisX
    {
        private int _size;

        public List<Vector> Vb;

        public AxisX(int size=100)
        {
            _size = size;

            Vb = new List<Vector> {new Vector(0, 0), new Vector(size, 0)};
        }

        public void Draw(Graphics g, List<Vector> vb)
        {
            var pen = new Pen(Color.Red, 2f);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
            var font = new Font("Arial", 10);
            var p = new PointF(vb[1].X, vb[1].Y);
            g.DrawString("x", font, Brushes.Red, p);
        }
    }
}
