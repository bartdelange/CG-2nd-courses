using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        private readonly AxisX _xAxis;
        private readonly AxisY _yAxis;
        private readonly Square _square;
        private readonly Square _square2;
        private readonly Square _square3;

        public Form1()
        {
            InitializeComponent();

            var v1 = new Vector();
            Console.WriteLine(v1);
            var v2 = new Vector(1, 2);
            Console.WriteLine(v2);
            var v3 = new Vector(2, 6);
            Console.WriteLine(v3);
            var v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            var m1 = new Matrix();
            Console.WriteLine(m1); // 1, 0, 0, 1
            var m2 = new Matrix(
                2, 4,
                -1, 3);
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16
	
            Width = 800;
            Height = 600;

            _xAxis = new AxisX(200);
            _yAxis = new AxisY(200);
            _square = new Square(Color.Purple,100);
            _square2 = new Square(Color.Orange,100);
            _square3 = new Square(Color.SaddleBrown,100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _xAxis.Draw(e.Graphics, ViewPortTransformation(Width, Height, _xAxis.Vb));
            _yAxis.Draw(e.Graphics, ViewPortTransformation(Width, Height, _yAxis.Vb));
            _square.Draw(e.Graphics, ViewPortTransformation(Width, Height, _square.Vb));

            var scale = Matrix.Scale(1.5f);
            var vb = new List<Vector>();
            foreach (var vector in _square2.Vb)
            {
                vb.Add(scale * vector);
            }
            _square2.Draw(e.Graphics, ViewPortTransformation(Width, Height, vb));

            var rotate = Matrix.Rotate((float) (Math.PI / 4));
            Console.WriteLine(rotate);
            vb = new List<Vector>();
            foreach (var vector in _square3.Vb)
            {
                var newVector = rotate * vector;
                Console.WriteLine(newVector);
                vb.Add(newVector);
            }
            _square3.Draw(e.Graphics, ViewPortTransformation(Width, Height, vb));
        }

        public static List<Vector> ViewPortTransformation(float width, float height, List<Vector> vectors)
        {
            var result = new List<Vector>();

            var dX = width / 2;
            var dY = height / 2;

            foreach (var vector in vectors)
            {
                result.Add(new Vector(vector.X + dX, dY - vector.Y));
            }

            return result;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}
