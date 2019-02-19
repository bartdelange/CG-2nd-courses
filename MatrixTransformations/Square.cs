using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    // Added in Lecture 3.1
    public class Square
    {
        private readonly Color _color;
        private int _size;
        private readonly float _weight;

        public List<Vector> Vb;

        public Square(Color color, int size = 100, float weight = 3)
        {
            _color = color;
            _size = size;
            _weight = weight;

            Vb = new List<Vector>
            {
                new Vector(-size, -size, 1), 
                new Vector(size, -size, 1), 
                new Vector(size, size, 1), 
                new Vector(-size, size, 1)
            };
        }

        public void Draw(Graphics g, List<Vector> vb)
        {
            var pen = new Pen(_color, _weight);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[2].X, vb[2].Y);
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[3].X, vb[3].Y);
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[0].X, vb[0].Y);
        }
    }
}
