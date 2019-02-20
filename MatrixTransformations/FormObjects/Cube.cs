using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MatrixTransformations
{
    public class Cube
    {
        //          7----------4
        //         /|         /|
        //        / |        / |                y
        //       /  6-------/--5                |
        //      3----------0  /                 ----x
        //      | /        | /                 /
        //      |/         |/                  z
        //      2----------1

        private const int Size = 1;

        public List<Vector> Vb = new List<Vector>
        {
            new Vector(1.0f, 1.0f, 1.0f, 1), //0
            new Vector(1.0f, -1.0f, 1.0f, 1), //1
            new Vector(-1.0f, -1.0f, 1.0f, 1), //2
            new Vector(-1.0f, 1.0f, 1.0f, 1), //3

            new Vector(1.0f, 1.0f, -1.0f, 1), //4
            new Vector(1.0f, -1.0f, -1.0f, 1), //5
            new Vector(-1.0f, -1.0f, -1.0f, 1), //6
            new Vector(-1.0f, 1.0f, -1.0f, 1), //7

            new Vector(1.2f, 1.2f, 1.2f, 1), //0
            new Vector(1.2f, -1.2f, 1.2f, 1), //1
            new Vector(-1.2f, -1.2f, 1.2f, 1), //2
            new Vector(-1.2f, 1.2f, 1.2f, 1), //3

            new Vector(1.2f, 1.2f, -1.2f, 1), //4
            new Vector(1.2f, -1.2f, -1.2f, 1), //5
            new Vector(-1.2f, -1.2f, -1.2f, 1), //6
            new Vector(-1.2f, 1.2f, -1.2f, 1) //7
        };

        private readonly Color _col;

        public Cube(Color c)
        {
            _col = c;
        }

        public void Draw(Graphics g, List<Vector> vb)
        {
            var pen = new Pen(_col, 2f);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y); //1 -> 2
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[2].X, vb[2].Y); //2 -> 3
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[3].X, vb[3].Y); //3 -> 4
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[0].X, vb[0].Y); //4 -> 1

            g.DrawLine(pen, vb[4].X, vb[4].Y, vb[5].X, vb[5].Y); //5 -> 6
            g.DrawLine(pen, vb[5].X, vb[5].Y, vb[6].X, vb[6].Y); //6 -> 7
            g.DrawLine(pen, vb[6].X, vb[6].Y, vb[7].X, vb[7].Y); //7 -> 8
            g.DrawLine(pen, vb[7].X, vb[7].Y, vb[4].X, vb[4].Y); //8 -> 5

            pen.DashStyle = DashStyle.DashDot;
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[4].X, vb[4].Y); //1 -> 5
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[5].X, vb[5].Y); //2 -> 6
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[6].X, vb[6].Y); //3 -> 7
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[7].X, vb[7].Y); //4 -> 8

            var font = new Font("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < 8; i++)
            {
                var p = new PointF(vb[i + 8].X, vb[i + 8].Y);
                g.DrawString(i.ToString(), font, Brushes.Black, p);
            }
        }
    }
}