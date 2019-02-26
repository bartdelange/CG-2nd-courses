using System;

namespace MatrixTransformations
{
    public class CubeAnimator
    {
        public CubeAnimator(Form1 form)
        {
            Form = form;
        }

        private Form1 Form { get; }

        public void DoAnimation()
        {
            if (Animating)
            {
                switch (Phase)
                {
                    case 1:
                        DoPhase1();
                        break;
                    case 2:
                        DoPhase2();
                        break;
                    case 3:
                        DoPhase3();
                        break;
                    case 4:
                        DoPhase4();
                        break;
                }
            }
        }

        public bool Animating { get; set; }
        public float ScaleStep = 0.01f;
        public float XRotationStep = 1f;
        public float YRotationStep = 1f;
        public int Phase { get; set; }

        private void DoPhase1()
        {
            Form.Theta -= 1;
            Form.CurrentScale += ScaleStep;
            Form.Scale = Matrix.Scale(Form.CurrentScale);
            if (ScaleStep > 0)
            {
                if (Form.CurrentScale >= 1.49)
                    ScaleStep = -ScaleStep;
            }
            else if (ScaleStep < 0)
            {
                if (Form.CurrentScale <= 1.0)
                {
                    ScaleStep = -ScaleStep;
                    Phase = 2;
                }
            }
        }

        private void DoPhase2()
        {
            Form.Theta -= 1;
//            Form.Rotation *= Matrix.RotateX((float) Math.PI / 180 * XRotationStep);
            Form.XDegrees += XRotationStep;
            var degree = Form.XDegrees;
            if (XRotationStep > 0)
            {
                if (degree >= 45 || degree <= -45) // if below 45 degrees
                    XRotationStep = -XRotationStep;
            }
            else if (XRotationStep < 0)
            {
                if (Math.Abs(degree) < 0.000001) // if 0 degrees again
                {
                    XRotationStep = -XRotationStep;
                    Phase = 3;
                }
            }
        }

        private void DoPhase3()
        {
            Form.Phi += 1;
//            Form.Rotation *= Matrix.RotateY((float) Math.PI / 180 * YRotationStep);
            Form.YDegrees += YRotationStep;
            var degree = Form.YDegrees;
            if (YRotationStep > 0)
            {
                if (degree >= 45 || degree <= -45) // if below 45 degrees
                    YRotationStep = -YRotationStep;
            }
            else if (YRotationStep < 0)
            {
                if (Math.Abs(degree) < 0.000001) // if 0 degrees again
                {
                    YRotationStep = -YRotationStep;
                    Phase = 4;
                }
            }
        }

        private void DoPhase4()
        {
            if (Math.Abs(Form.Theta - Form1.InitTheta) < 0.000001 && Math.Abs(Form.Phi - Form1.InitPhi) < 0.000001)
            {
                Phase = 1;
            }
            else
            {
                if (Math.Abs(Form.Theta - Form1.InitTheta) > 0.000001)
                    Form.Theta += 1;
                if (Math.Abs(Form.Phi - Form1.InitPhi) > 0.000001)
                    Form.Phi -= 1;
            }
        }
    }
}