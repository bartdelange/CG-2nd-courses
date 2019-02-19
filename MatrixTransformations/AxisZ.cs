using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    public class AxisZ
    {
        public readonly List<Vector> Vb;

        public AxisZ(int size = 100)
        {
            Vb = new List<Vector>
            {
                new Vector(0, 0, 0, 1), 
                new Vector(0, 0, size, 1)
            };
        }

        public void Draw(Graphics g, List<Vector> vb)
        {
            var pen = new Pen(Color.Blue, 2f);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
            var font = new Font("Arial", 10);
            var p = new PointF(vb[1].X, vb[1].Y);
            g.DrawString("y", font, Brushes.Blue, p);
        }
    }
}
