using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        private const float InitR = 10;
        private const float InitTheta = -100f;
        private const float InitPhi = -10f;

        private float R { get; set; }
        private float D { get; set; }
        private float Theta { get; set; }
        private float Phi { get; set; }

        private Matrix Rotation { get; set; }
        private Matrix Scale { get; set; }
        private Matrix Translation { get; set; }

        private readonly AxisX _xAxis;
        private readonly AxisY _yAxis;
        private readonly AxisZ _zAxis;
        private readonly Cube _cube;
        private float _scale;

        public Form1()
        {
            InitializeComponent();

            Width = 800;
            Height = 600;

            _xAxis = new AxisX(5);
            _yAxis = new AxisY(5);
            _zAxis = new AxisZ(5);
            _cube = new Cube(Color.Purple);

            Reset();
        }

        private void Reset()
        {
            Scale = Matrix.Identity();
            Rotation = Matrix.Identity();
            Translation = Matrix.Identity();
            _scale = 1;

            R = InitR;
            D = 500;
            Theta = InitTheta;
            Phi = InitPhi;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PrintHelpText(e);
            
            _xAxis.Draw(e.Graphics, ViewportTransformation(Width, Height, _xAxis.Vb).ToList());
            _yAxis.Draw(e.Graphics, ViewportTransformation(Width, Height, _yAxis.Vb).ToList());
            _zAxis.Draw(e.Graphics, ViewportTransformation(Width, Height, _zAxis.Vb).ToList());

            var vb = new List<Vector>();
            foreach (var vector in _cube.Vb)
            {
                var v = Translation * Rotation * Scale * vector;
                vb.Add(v);
            }
            _cube.Draw(e.Graphics, ViewportTransformation(Width, Height, vb).ToList());
        }

        private IEnumerable<Vector> ViewportTransformation(float width, float height, IEnumerable<Vector> vb)
        {
            foreach (var vector in vb)
            {
                var view = Matrix.ViewPort(R,(float) Math.PI / 180 * Phi, (float) Math.PI / 180 * Theta) * vector;
                var projection = Matrix.Project(D, view.Z) * view;
                yield return new Vector(projection.X + width / 2, -projection.Y + height / 2, 0, 1);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
            
            if (e.KeyCode == Keys.C)
            {
                Reset();
            }
                        
            switch (e.KeyCode)
            {
                case Keys.A: // Reset all
                    Reset();
                    break;

                case Keys.Left: // Move x down
                    Translation *= Matrix.Translate(new Vector(-.1f, 0f, 0f, 1f));
                    break;
                case Keys.Right: // Move x up
                    Translation *= Matrix.Translate(new Vector(.1f, 0f, 0f, 1f)); 
                    break;
                case Keys.Up: // Move y down
                    Translation *= Matrix.Translate(new Vector(0, .1f, 0f, 1f)); 
                    break;
                case Keys.Down: // Move y up
                    Translation *= Matrix.Translate(new Vector(0f, -.1f, 0f, 1f));
                    break;
                case Keys.PageUp: // Move z down
                    Translation *= Matrix.Translate(new Vector(0f, 0f, -.1f, 1f));
                    break;
                case Keys.PageDown: // Move z up
                    Translation *= Matrix.Translate(new Vector(0f, 0f, .1f, 1f));
                    break;

                case Keys.X: // Rotate x
                    if (e.Modifiers == Keys.Shift)
                        Rotation *= Matrix.RotateX((float) Math.PI / 180 * -1);
                    else
                        Rotation *= Matrix.RotateX((float) Math.PI / 180 * 1);
                    break;
                case Keys.Y: // Rotate y
                    if (e.Modifiers == Keys.Shift)
                        Rotation *= Matrix.RotateY((float) Math.PI / 180 * -1);
                    else
                        Rotation *= Matrix.RotateY((float) Math.PI / 180 * 1);
                    break;
                case Keys.Z: // Rotate z
                    if (e.Modifiers == Keys.Shift)
                        Rotation *= Matrix.RotateZ((float) Math.PI / 180 * -1);
                    else
                        Rotation *= Matrix.RotateZ((float) Math.PI / 180 * 1);
                    break;

                case Keys.S: // Adjust scale
                    if (e.Modifiers == Keys.Shift)
                        _scale -= .1f;
                    else
                        _scale += .1f;
                    
                    Scale = Matrix.Scale(_scale);
                    break;

                case Keys.R: // Modify R
                    if (e.Modifiers == Keys.Shift)
                        R -= 1f;
                    else
                        R += 1f;
                    break;
                case Keys.P: // Adjust Phi
                    if (e.Modifiers == Keys.Shift)
                        Phi -= 1f;
                    else
                        Phi += 1f;
                    break;
                case Keys.T: // Adjust Theta
                    if (e.Modifiers == Keys.Shift)
                        Theta -= 1f;
                    else
                        Theta += 1f;
                    break;
                case Keys.D: // Adjust distance
                    if (e.Modifiers == Keys.Shift)
                        D -= 1f;
                    else
                        D += 1f;
                    break;
            }
            
            Invalidate(); // Repaint
        }

        public void PrintHelpText(PaintEventArgs e)
        {
            var keys = "A: Reset all\n" +
                       "Left: Move x up\n" +
                       "Right: Move x down\n" +
                       "Up: Move y up\n" +
                       "Down: Move y down\n" +
                       "PageUp: Move z up\n" +
                       "PageDown: Move z down\n" +
                       "x/X: Rotate x\n" +
                       "y/Y: Rotate y\n" +
                       "z/Z: Rotate z\n" +
                       "s/S: Adjust scale\n" +
                       "r/R: Modify R\n" +
                       "p/P: Adjust Phi\n" +
                       "t/T: Adjust Theta\n" +
                       "d/D: Adjust distance\n";
            
            e.Graphics.DrawString(keys, new Font("Arial", 12), Brushes.Black, 20, Height / 3);
            
            var stats = $"s:     {_scale}\n" +
                        $"r:     {R}\n" +
                        $"d:     {D}\n" +
                        $"phi:   {Phi}\n" +
                        $"theta: {Theta}\n" +
                        $"phase: animphase\n";
            
            e.Graphics.DrawString(stats, new Font("Arial", 12), Brushes.Black, 20, 20);
        }
    }
}