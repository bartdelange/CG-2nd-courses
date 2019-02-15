using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatrixTransformations;

namespace MatrixTransformationsTests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void Test_Create_WithParameters()
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
        public void Test_Create_WithoutParameters()
        {
            var testMatrix = new Matrix();
            
            Assert.AreEqual(testMatrix[0, 0], 1);
            Assert.AreEqual(testMatrix[0, 1], 0);
            Assert.AreEqual(testMatrix[1, 0], 0);
            Assert.AreEqual(testMatrix[1, 1], 1);
        }

        [TestMethod]
        public void Test_Addition()
        {
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
            
            var result = left + right;
        
            Assert.AreEqual(result[0, 0], 6);
            Assert.AreEqual(result[0, 1], 8);
            Assert.AreEqual(result[1, 0], 10);
            Assert.AreEqual(result[1, 1], 12);
        }

        [TestMethod]
        public void Test_Subtraction()
        {   
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
            
            var result = left - right;
            Console.WriteLine(result);
            
            Assert.AreEqual(1, result[0, 0]);
            Assert.AreEqual(2, result[0, 1]);
            Assert.AreEqual(3, result[1, 0]);
            Assert.AreEqual(4, result[1, 1]);
        }

        [TestMethod]
        public void Test_Float_Multiplication()
        {
            var mtrx = Matrix.Identity();                
            mtrx[0, 0] = 1f;
            mtrx[0, 1] = 2f;
            mtrx[1, 0] = 3f;
            mtrx[1, 1] = 4f;

            const float flt = 2.0f;
            
            var result1 = mtrx * flt;
            var result2 = flt * mtrx;
            
            Assert.AreEqual(2, result1[0, 0]);
            Assert.AreEqual(4, result1[0, 1]);
            Assert.AreEqual(6, result1[1, 0]);
            Assert.AreEqual(8, result1[1, 1]);
            
            Assert.AreEqual(2, result2[0, 0]);
            Assert.AreEqual(4, result2[0, 1]);
            Assert.AreEqual(6, result2[1, 0]);
            Assert.AreEqual(8, result2[1, 1]);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication()
        {
            
            var left = Matrix.Identity();
            left[0, 0] = 1f;
            left[0, 1] = 2f;
            left[1, 0] = 3f;
            left[1, 1] = 4f;
            
            var right = Matrix.Identity();
            left[0, 0] = 1f;
            left[0, 1] = 2f;
            left[1, 0] = 3f;
            left[1, 1] = 4f;

            
            var result = left * right;
            
            Assert.AreEqual(7,result[0, 0]);
            Assert.AreEqual(10,result[0, 1]);
            Assert.AreEqual(15,result[1, 0]);
            Assert.AreEqual(22, result[1, 1]);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication2()
        {
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
            
            var result = left * right;
            
            Assert.AreEqual(19, result[0, 0]);
            Assert.AreEqual(22, result[0, 1]);
            Assert.AreEqual(43, result[1, 0]);
            Assert.AreEqual(50, result[1, 1]);
        }

        [TestMethod]
        public void Test_Vector_Multiplication()
        {
            var m = Matrix.Identity();
            m[0, 0] = 1f;
            m[0, 1] = 2f;
            m[1, 0] = 3f;
            m[1, 1] = 4f;

            var v = new Vector(2, 2); 
            
            var result = m * v;
            
            Assert.AreEqual(19, result.X);
            Assert.AreEqual(22, result.Y);
            Assert.AreEqual(43, result.W);
            Assert.AreEqual(50, result.Z);
        }
    }
}