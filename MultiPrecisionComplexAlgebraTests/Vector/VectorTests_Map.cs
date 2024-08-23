using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplexAlgebra;
using System;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod()]
        public void FuncTest() {
            ComplexVector<Pow2.N4> vector1 = new(1, 2, 4, 8);
            ComplexVector<Pow2.N4> vector2 = new(2, 3, 5, 9);
            ComplexVector<Pow2.N4> vector3 = new(3, 4, 6, 10);
            ComplexVector<Pow2.N4> vector4 = new(4, 5, 7, 11);
            ComplexVector<Pow2.N4> vector5 = new(5, 6, 8, 12, 20);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(2, 4, 8, 16), ComplexVector<Pow2.N4>.Func(v => 2 * v, vector1));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(5, 8, 14, 26), ComplexVector<Pow2.N4>.Func((v1, v2) => v1 + 2 * v2, vector1, vector2));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(17, 24, 38, 66), ComplexVector<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector2, vector3));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(49, 64, 94, 154), ComplexVector<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(49, 64, 94, 154), ComplexVector<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2) => v1 + 2 * v2, vector1, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2) => v1 + 2 * v2, vector5, vector1);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector2, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector5, vector2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector5, vector1, vector2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector5, vector3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector5, vector2, vector3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector5, vector1, vector2, vector3);
            });
        }
    }
}