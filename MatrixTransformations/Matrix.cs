﻿using System;

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

        public Matrix() : this(
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f
        )
        {
        }

        public Matrix(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
        )
        {
            _mat = new float[4, 4];
            
            _mat[0, 0] = m11;
            _mat[0, 1] = m12;
            _mat[0, 2] = m13;
            _mat[0, 3] = m14;
            
            _mat[1, 0] = m21;
            _mat[1, 1] = m22;
            _mat[1, 2] = m23;
            _mat[1, 3] = m24;
            
            _mat[2, 0] = m31;
            _mat[2, 1] = m32;
            _mat[2, 2] = m33;
            _mat[2, 3] = m34;
            
            _mat[3, 0] = m41;
            _mat[3, 1] = m42;
            _mat[3, 2] = m43;
            _mat[3, 3] = m44;
        }

        public Matrix(Vector v) : this(
            v.X, 0f, 0f, 0f,
            v.Y, 1f, 0f, 0f,
            v.W, 0f, 1f, 0f,
            v.Z, 0f, 0f, 1f
        )
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
            var mat = s * Identity();
            mat[3,3] = 1;
            return mat;
        }

        public static Matrix Rotate(float degrees)
        {
            var radians = degrees * (Math.PI / 180);

            return new Matrix(
                (float) Math.Cos(radians), (float) -Math.Sin(radians), 0f, 0f,
                (float) Math.Sin(radians), (float) Math.Cos(radians), 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f
            );
        }

        public static Matrix Translate(Vector t)
        {
            var matrix = Identity();
            matrix[0, 2] = t.X;
            matrix[1, 2] = t.Y;
            matrix[2, 2] = t.Z;
            return matrix;
        }

        public override string ToString()
        {
            var str = "{\n";

            for (var r = 0; r < Rows; r++)
            {
                str += "    { ";
                var row = " ";
                for (var c = 0; c < Columns; c++)
                {
                    row += _mat[r, c] + ", ";
                }

                str += row.Substring(0, row.Length-2) + " },\n";
            }
            
            str = str.Substring(0, str.Length-2) + "\n";

            return str + "}";
        }
    }
}