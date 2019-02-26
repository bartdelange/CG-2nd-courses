using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        public const float InitR = 10;
        public const float InitTheta = -100f;
        public const float InitPhi = -10f;

        public float R { get; set; }
        public float D { get; set; }
        public float Theta { get; set; }
        public float Phi { get; set; }

        public double XDegrees { get; set; }
        public double YDegrees { get; set; }
        public double ZDegrees { get; set; }

        public Matrix Scale { get; set; }
        public Matrix Translation { get; set; }
        public float CurrentScale { get; set; }

        private readonly AxisX _xAxis;
        private readonly AxisY _yAxis;
        private readonly AxisZ _zAxis;
        private readonly Cube _cube;
        private readonly CubeAnimator _anim;

        private static readonly Timer MyTimer = new Timer();

        private void TimerEventProcessor(object myObject,
            EventArgs myEventArgs)
        {
            // Repaint and animation
            _anim.DoAnimation();
            Invalidate();
        }

        public Form1()
        {
            InitializeComponent();

            MyTimer.Tick += TimerEventProcessor;
            MyTimer.Interval = 1000 / 30; // 1 sec / fps
            MyTimer.Start();

            DoubleBuffered = true;

            Width = 800;
            Height = 600;

            _xAxis = new AxisX(3);
            _yAxis = new AxisY(3);
            _zAxis = new AxisZ(3);
            _cube = new Cube(Color.Purple);

            _anim = new CubeAnimator(this);

            Reset();
        }

        private void Reset()
        {
            Scale = Matrix.Identity();
            XDegrees = 0;
            YDegrees = 0;
            ZDegrees = 0;
            Translation = Matrix.Identity();
            CurrentScale = 1;

            R = InitR;
            D = 800;
            Theta = InitTheta;
            Phi = InitPhi;

            // animation values
            _anim.Animating = false;
            _anim.ScaleStep = 0.01f;
            _anim.XRotationStep = 1f;
            _anim.YRotationStep = 1f;
            _anim.Phase = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PrintHelpText(e);

            _xAxis.Draw(e.Graphics, ViewportTransformation(Width, Height, _xAxis.Vb).ToList());
            _yAxis.Draw(e.Graphics, ViewportTransformation(Width, Height, _yAxis.Vb).ToList());
            _zAxis.Draw(e.Graphics, ViewportTransformation(Width, Height, _zAxis.Vb).ToList());

            var rotation = Matrix.RotateX((float) (Math.PI / 180 * XDegrees));
            rotation *= Matrix.RotateZ((float) (Math.PI / 180 * ZDegrees));
            rotation *= Matrix.RotateY((float) (Math.PI / 180 * YDegrees));

            var vb = new List<Vector>();
            foreach (var vector in _cube.Vb)
            {
                var v = Translation * Matrix.RotateX((float) (Math.PI / 180 * XDegrees)) *
                        Matrix.RotateY((float) (Math.PI / 180 * YDegrees)) *
                        Matrix.RotateZ((float) (Math.PI / 180 * ZDegrees)) * Scale * vector;
                vb.Add(v);
            }

            _cube.Draw(e.Graphics, ViewportTransformation(Width, Height, vb).ToList());
        }

        private IEnumerable<Vector> ViewportTransformation(float width, float height, IEnumerable<Vector> vb)
        {
            foreach (var vector in vb)
            {
                var view = Matrix.ViewPort(R, (float) Math.PI / 180 * Phi, (float) Math.PI / 180 * Theta) * vector;
                var projection = Matrix.Project(D, view.Z) * view;
                yield return new Vector(projection.X + width / 2, -projection.Y + height / 2, 0, 1);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            switch (e.KeyCode)
            {
                case Keys.A: // Animate
                    Reset();
                    _anim.Phase = 1;
                    _anim.Animating = true;
                    break;
                case Keys.C: // Reset all
                    _anim.Animating = false;
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
                        XDegrees--;
                    else
                        XDegrees++;
                    break;
                case Keys.Y: // Rotate y
                    if (e.Modifiers == Keys.Shift)
                        YDegrees--;
                    else
                        YDegrees++;
                    break;
                case Keys.Z: // Rotate z
                    if (e.Modifiers == Keys.Shift)
                        ZDegrees--;
                    else
                        ZDegrees++;
                    break;

                case Keys.S: // Adjust scale
                    if (e.Modifiers == Keys.Shift)
                        CurrentScale -= .1f;
                    else
                        CurrentScale += .1f;
                    Scale = Matrix.Scale(CurrentScale);
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
        }

        private void PrintHelpText(PaintEventArgs e)
        {
            var stats = "STATS:\n" +
                        $"S: \t\t {CurrentScale}\n" +
                        $"R: \t\t {R}\n" +
                        $"D: \t\t {D}\n" +
                        $"Phi: \t\t {Phi}°\n" +
                        $"Theta: \t\t {Theta}°\n" +
                        $"X Rotation: \t {XDegrees}°\n" +
                        $"Y Rotation: \t {YDegrees}°\n" +
                        $"Z Rotation: \t {ZDegrees}°\n" +
                        $"Anim phase: \t {_anim.Phase}\n";

            var keysInfo = "KEY BINDINGS:\n" +
                           "A: \t\t Start Animation\n" +
                           "C: \t\t Reset all\n" +
                           "Left: \t\t Move x up\n" +
                           "Right: \t\t Move x down\n" +
                           "Up: \t\t Move y up\n" +
                           "Down: \t\t Move y down\n" +
                           "PageUp: \t Move z up\n" +
                           "PageDown: \t Move z down\n" +
                           "x/X: \t\t Rotate x\n" +
                           "y/Y: \t\t Rotate y\n" +
                           "z/Z: \t\t Rotate z\n" +
                           "s/S: \t\t Adjust scale\n" +
                           "r/R: \t\t Modify R\n" +
                           "p/P: \t\t Adjust Phi\n" +
                           "t/T: \t\t Adjust Theta\n" +
                           "d/D: \t\t Adjust distance\n";

            var helpText = stats + "\n\n" + keysInfo;

            e.Graphics.DrawString(helpText, new Font("Arial", 10), Brushes.Black, 20, 20);
        }
    }
}