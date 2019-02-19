using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatrixTransformations;

namespace MatrixTransformationsTests
{
    [TestClass]
    public class MatrixTest
    {
        #region Utils

        private static void AssertMatrix(Matrix expected, Matrix result, float delta = 0.000001f)
        {
            for (var r = 0; r < 4; r++)
            {
                for (var c = 0; c < 4; c++)
                {
                    Assert.AreEqual(expected[r, c], result[r, c], delta, $"Position[{r}, {c}]");
                }
            }
        }

        #endregion

        #region Creation tests

        [TestMethod]
        public void Test_CreateWithParameters()
        {
            var testMatrix = new Matrix(
                1f, 2f, 0f, 0f,
                3f, 4f, 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f
            );

            Assert.AreEqual(testMatrix[0, 0], 1);
            Assert.AreEqual(testMatrix[0, 1], 2);
            Assert.AreEqual(testMatrix[1, 0], 3);
            Assert.AreEqual(testMatrix[1, 1], 4);
        }

        [TestMethod]
        public void Test_CreateWithoutParameters()
        {
            var testMatrix = new Matrix();

            Assert.AreEqual(testMatrix[0, 0], 1);
            Assert.AreEqual(testMatrix[0, 1], 0);
            Assert.AreEqual(testMatrix[1, 0], 0);
            Assert.AreEqual(testMatrix[1, 1], 1);
        }

        [TestMethod]
        public void Test_CreateFromVector()
        {
            var testMatrix = new Matrix();

            Assert.AreEqual(testMatrix[0, 0], 1);
            Assert.AreEqual(testMatrix[0, 1], 0);
            Assert.AreEqual(testMatrix[1, 0], 0);
            Assert.AreEqual(testMatrix[1, 1], 1);
        }

        #endregion

        #region Calculation tests

        [TestMethod]
        public void Test_Addition()
        {
            // Setup
            var left = Matrix.Identity();
            left[0, 0] = 1f;
            left[0, 1] = 2f;
            left[1, 0] = 3f;
            left[1, 1] = 4f;
            var right = Matrix.Identity();
            right[0, 0] = 5f;
            right[0, 1] = 6f;
            right[1, 0] = 7f;
            right[1, 1] = 8f;

            // Act
            var result = left + right;

            // Assert
            var expected = new Matrix(
                6f, 8f, 0f, 0f,
                10f, 12f, 0f, 0f,
                0f, 0f, 2f, 0f,
                0f, 0f, 0f, 2f
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_Subtraction()
        {
            // Setup
            var left = Matrix.Identity();
            left[0, 0] = 6f;
            left[0, 1] = 7f;
            left[1, 0] = 10f;
            left[1, 1] = 12f;

            var right = Matrix.Identity();
            right[0, 0] = 5f;
            right[0, 1] = 6f;
            right[1, 0] = 7f;
            right[1, 1] = 8f;

            // Act
            var result = left - right;

            // Assert
            var expected = new Matrix(
                1f, 1f, 0f, 0f,
                3f, 4f, 0f, 0f,
                0f, 0f, 0f, 0f,
                0f, 0f, 0f, 0f
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_Float_Multiplication()
        {
            // Setup
            var mtrx = Matrix.Identity();
            mtrx[0, 0] = 1f;
            mtrx[0, 1] = 2f;
            mtrx[1, 0] = 3f;
            mtrx[1, 1] = 4f;
            const float flt = 2f;

            // Act
            var result1 = mtrx * flt;
            var result2 = flt * mtrx;

            // Assert
            var expected = new Matrix(
                2f, 4f, 0f, 0f,
                6f, 8f, 0f, 0f,
                0f, 0f, 2f, 0f,
                0f, 0f, 0f, 2f
            );
            AssertMatrix(expected, result1);
            AssertMatrix(expected, result2);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication()
        {
            // Setup
            var left = Matrix.Identity();
            left[0, 0] = 1f;
            left[0, 1] = 2f;
            left[1, 0] = 3f;
            left[1, 1] = 4f;
            var right = Matrix.Identity();
            right[0, 0] = 1f;
            right[0, 1] = 2f;
            right[1, 0] = 3f;
            right[1, 1] = 4f;

            // Act
            var result = left * right;

            // Assert
            var expected = new Matrix(
                7f, 10f, 0f, 0f,
                15f, 22f, 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication2()
        {
            // Setup
            var left = Matrix.Identity();
            left[0, 0] = 1f;
            left[0, 1] = 2f;
            left[1, 0] = 3f;
            left[1, 1] = 4f;
            var right = Matrix.Identity();
            right[0, 0] = 5f;
            right[0, 1] = 6f;
            right[1, 0] = 7f;
            right[1, 1] = 8f;

            // Act
            var result = left * right;

            // Assert
            var expected = new Matrix(
                19f, 22f, 0f, 0f,
                43f, 50f, 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication3()
        {
            // Setup
            var left = new Matrix(
                1f, 2f, 3f, 4f,
                2f, 1f, 2f, 3f,
                3f, 2f, 1f, 2f,
                4f, 3f, 2f, 1f
            );
            var right = new Matrix(
                2f, 3f, 4f, 5f,
                3f, 2f, 3f, 4f,
                4f, 3f, 2f, 3f,
                5f, 4f, 3f, 2f
            );

            // Act
            var result = left * right;

            // Assert
            var expected = new Matrix(
                40f, 32f, 28f, 30f,
                30f, 26f, 24f, 26f,
                26f, 24f, 26f, 30f,
                30f, 28f, 32f, 40f
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_Vector_Multiplication()
        {
            var m = Matrix.Identity();
            m[0, 0] = 1f;
            m[0, 1] = 2f;
            m[1, 0] = 3f;
            m[1, 1] = 4f;

            var v = new Vector(2, 2, 0, 1);

            var result = m * v;

            Assert.AreEqual(6, result.X);
            Assert.AreEqual(14, result.Y);
            Assert.AreEqual(0, result.Z);
            Assert.AreEqual(1, result.W);
        }

        #endregion

        #region Transformation tests

        [TestMethod]
        public void Test_MatrixScale()
        {
            // Setup/Act
            var result = Matrix.Scale(4);
            
            // Assert
            var expected = new Matrix(
                4, 0, 0, 0,
                0, 4, 0, 0,
                0, 0, 4, 0,
                0, 0, 0, 1
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_MatrixRotateX()
        {
            // Setup/Act
            var result = Matrix.RotateX((float) Math.PI);
            
            // Assert
            var expected = new Matrix(
                1, 0, 0, 0,
                0, -1, 0, 0,
                0, 0, -1, 0,
                0, 0, 0, 1
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_MatrixRotateY()
        {
            // Setup/Act
            var result = Matrix.RotateY((float) Math.PI);
            
            // Assert
            var expected = new Matrix(
                -1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, -1, 0,
                0, 0, 0, 1
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_MatrixRotateZ()
        {
            // Setup/Act
            var result = Matrix.RotateZ((float) Math.PI);
            
            // Assert
            var expected = new Matrix(
                -1, 0, 0, 0,
                0, -1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
            AssertMatrix(expected, result);
        }

        [TestMethod]
        public void Test_MatrixTranslate()
        {
            // Setup/Act
            var result = Matrix.Translate(new Vector(1, 1, 1, 1));
            
            // Assert
            var expected = new Matrix(
                1, 0, 0, 1,
                0, 1, 0, 1,
                0, 0, 1, 1,
                0, 0, 0, 1
            );
            AssertMatrix(expected, result);
        }

        #endregion
        
    }
}