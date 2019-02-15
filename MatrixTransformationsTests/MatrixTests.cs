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
            var testMatrix = new Matrix(1, 2, 3, 4);
            
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
            var left = new Matrix(1, 2, 3, 4);
            var right = new Matrix(5, 6, 7, 8);
            
            var result = left + right;
        
            Assert.AreEqual(result[0, 0], 6);
            Assert.AreEqual(result[0, 1], 8);
            Assert.AreEqual(result[1, 0], 10);
            Assert.AreEqual(result[1, 1], 12);
        }

        [TestMethod]
        public void Test_Subtraction()
        {
            var left = new Matrix(6, 8, 10, 12);
            var right = new Matrix(5, 6, 7, 8);
            
            var result = left - right;
            
            Assert.AreEqual(result[0, 0], 1);
            Assert.AreEqual(result[0, 1], 1);
            Assert.AreEqual(result[1, 0], 3);
            Assert.AreEqual(result[1, 1], 4);
        }

        [TestMethod]
        public void Test_Float_Multiplication()
        {
            var mtrx = new Matrix(1, 2, 3, 4);
            const float flt = 2.0f;
            
            var result1 = mtrx * flt;
            var result2 = flt * mtrx;
            
            Assert.AreEqual(result1[0, 0], 2);
            Assert.AreEqual(result1[0, 1], 4);
            Assert.AreEqual(result1[1, 0], 6);
            Assert.AreEqual(result1[1, 1], 8);
            
            Assert.AreEqual(result2[0, 0], 2);
            Assert.AreEqual(result2[0, 1], 4);
            Assert.AreEqual(result2[1, 0], 6);
            Assert.AreEqual(result2[1, 1], 8);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication()
        {
            var mtrxL = new Matrix(1, 2, 3, 4);
            var mtrxR = new Matrix(1, 2, 3, 4);
            
            var result = mtrxL * mtrxR;
            
            Assert.AreEqual(result[0, 0], 7);
            Assert.AreEqual(result[0, 1], 10);
            Assert.AreEqual(result[1, 0], 15);
            Assert.AreEqual(result[1, 1], 20);
        }

        [TestMethod]
        public void Test_Matrix_Multiplication2()
        {
            var mtrxL = new Matrix(1, 2, 3, 4);
            var mtrxR = new Matrix(5, 6, 7, 8);
            
            var result = mtrxL * mtrxR;
            
            Assert.AreEqual(result[0, 0], 19);
            Assert.AreEqual(result[0, 1], 22);
            Assert.AreEqual(result[1, 0], 43);
            Assert.AreEqual(result[1, 1], 50);
        }

        [TestMethod]
        public void Test_Vector_Multiplication()
        {
//            var mtrx = new Matrix(1, 2, 3, 4);
//            const float flt = 2.0f;
//            
//            var result1 = mtrx * flt;
//            var result2 = flt * mtrx;
//            
//            Assert.AreEqual(result1[0, 0], 2);
//            Assert.AreEqual(result1[0, 1], 4);
//            Assert.AreEqual(result1[1, 0], 6);
//            Assert.AreEqual(result1[1, 1], 8);
        }
    }
}