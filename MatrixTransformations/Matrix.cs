using System;

namespace MatrixTransformations
{
    public class Matrix
    {
        private readonly float[,] _mat;

        public float this[int n, int m]
        {
            get => _mat[n, m];
            set => _mat[n, m] = value;
        }

        public int Rows => _mat.GetLength(0);

        public int Columns => _mat.GetLength(1);

        public Matrix() : this(1, 0, 0, 1)
        {
        }

        public Matrix(float m11, float m12,
            float m21, float m22)
        {
            _mat = new float[2, 2];
            _mat[0, 0] = m11;
            _mat[0, 1] = m12;
            _mat[1, 0] = m21;
            _mat[1, 1] = m22;
        }

        public Matrix(Vector v) : this(v.X, 0.0f, v.Y, 0.0f)
        {
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            var m3 = new Matrix();

            for (var c = 0; c < m3.Columns; ++c)
            {
                for (var r = 0; r < m3.Rows; ++r)
                {
                    m3[r, c] = m1[r, c] + m2[r, c];
                }
            }

            return m3;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            var m3 = new Matrix();

            for (var c = 0; c < m3.Columns; ++c)
            {
                for (var r = 0; r < m3.Rows; ++r)
                {
                    m3[r, c] = m1[r, c] - m2[r, c];
                }
            }

            return m3;
        }

        public static Matrix operator *(Matrix m1, float f)
        {
            var m3 = new Matrix();

            for (var c = 0; c < m3.Columns; ++c)
            {
                for (var r = 0; r < m3.Rows; ++r)
                {
                    m3[r, c] = m1[r, c] * f;
                }
            }

            return m3;
        }

        public static Matrix operator *(float f, Matrix m1)
        {
            return m1 * f;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            var m3 = new Matrix();
            
            for (var r = 0; r < m3.Rows; r++)
            {
                for (var c = 0; c < m3.Columns; c++)
                {
                    var m3CurrentSpot = 0.0f;

                    // Assuming our matrix is always square we can choose between rows and columns
                    for (var i = 0; i < m3.Rows; i++)
                    {
                        m3CurrentSpot += m1[r, i] * m2[i, c];
                    }

                    m3[r, c] = m3CurrentSpot;
                }
            }

            return m3;
        }

        public static Vector operator *(Matrix m1, Vector v)
        {
            var matrix = m1 * new Matrix(v);
            return matrix.ToVector();
        }

        public static Matrix Identity()
        {
            return new Matrix();
        }

        public Vector ToVector()
        {
            return new Vector(_mat[0, 0], _mat[1, 0]);
        }

        public static Matrix Scale(float s)
        {
            return s * Identity();
        }

        public static Matrix Rotate(float radians)
        {
            return new Matrix(
                (float) Math.Cos(radians), (float) -Math.Sin(radians),
                (float) Math.Sin(radians), (float) Math.Cos(radians)
            );
        }

        public override string ToString()
        {
            return "{\n" + "{" + _mat[0, 0] + ", " + _mat[0, 1] + "},\n" + "{" + _mat[1, 0] + ", " + _mat[1, 1] + "},\n}";
        }
    }
}