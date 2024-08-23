using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;
using System;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void FuncTest() {
            ComplexMatrix<Pow2.N4> matrix1 = new(new Complex<Pow2.N4>[,] { { 1, 2, 4 }, { 8, 16, 32 } });
            ComplexMatrix<Pow2.N4> matrix2 = new(new Complex<Pow2.N4>[,] { { 2, 3, 5 }, { 9, 17, 33 } });
            ComplexMatrix<Pow2.N4> matrix3 = new(new Complex<Pow2.N4>[,] { { 3, 4, 6 }, { 10, 18, 34 } });
            ComplexMatrix<Pow2.N4> matrix4 = new(new Complex<Pow2.N4>[,] { { 4, 5, 7 }, { 11, 19, 35 } });
            ComplexMatrix<Pow2.N4> matrix5 = new(new Complex<Pow2.N4>[,] { { 5, 6, 8 }, { 12, 20, 36 }, { 2, 1, 0 } });

            Assert.AreEqual(new ComplexMatrix<Pow2.N4>(new Complex<Pow2.N4>[,] { { 2, 4, 8 }, { 16, 32, 64 } }), (ComplexMatrix<Pow2.N4>)(v => 2 * v, matrix1));
            Assert.AreEqual(new ComplexMatrix<Pow2.N4>(new Complex<Pow2.N4>[,] { { 5, 8, 14 }, { 26, 50, 98 } }), (ComplexMatrix<Pow2.N4>)((v1, v2) => v1 + 2 * v2, (matrix1, matrix2)));
            Assert.AreEqual(new ComplexMatrix<Pow2.N4>(new Complex<Pow2.N4>[,] { { 17, 24, 38 }, { 66, 122, 234 } }), (ComplexMatrix<Pow2.N4>)((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, (matrix1, matrix2, matrix3)));
            Assert.AreEqual(new ComplexMatrix<Pow2.N4>(new Complex<Pow2.N4>[,] { { 49, 64, 94 }, { 154, 274, 514 } }), (ComplexMatrix<Pow2.N4>)((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, (matrix1, matrix2, matrix3, matrix4)));
            Assert.AreEqual(new ComplexMatrix<Pow2.N4>(new Complex<Pow2.N4>[,] { { 49, 64, 94 }, { 154, 274, 514 } }), (ComplexMatrix<Pow2.N4>)((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, (matrix1, matrix2, matrix3, matrix4)));

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2) => v1 + 2 * v2, matrix1, matrix5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2) => v1 + 2 * v2, matrix5, matrix1);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix1, matrix2, matrix5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix1, matrix5, matrix2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix5, matrix1, matrix2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix2, matrix3, matrix5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix2, matrix5, matrix3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix5, matrix2, matrix3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix5, matrix1, matrix2, matrix3);
            });
        }


        [TestMethod()]
        public void MapTest() {
            Vector<Pow2.N4> vector1 = new(1, 2, 4, 8);
            Vector<Pow2.N4> vector2 = new(2, 3, 5);

            ComplexMatrix<Pow2.N4> m = ((v1, v2) => v1 + v2, vector1, vector2);

            Assert.AreEqual(new ComplexMatrix<Pow2.N4>(new Complex<Pow2.N4>[,] { { 3, 4, 6 }, { 4, 5, 7 }, { 6, 7, 9 }, { 10, 11, 13 } }), m);
        }
    }
}