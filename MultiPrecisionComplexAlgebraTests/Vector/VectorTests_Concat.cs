using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod()]
        public void ConcatTest() {
            ComplexVector<Pow2.N4> vector1 = ComplexVector<Pow2.N4>.Fill(1, value: -1);
            ComplexVector<Pow2.N4> vector2 = ComplexVector<Pow2.N4>.Fill(2, value: -2);
            ComplexVector<Pow2.N4> vector4 = ComplexVector<Pow2.N4>.Fill(4, value: -3);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-2, -2, -3, -3, -3, -3, -1), ComplexVector<Pow2.N4>.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 1, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 2, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 3, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, (MultiPrecision<Pow2.N4>)3, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, Complex<Pow2.N4>.ImaginaryOne, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, Complex<Pow2.N4>.ImaginaryOne, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 4, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 5, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, "6.2", -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsExactly<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Concat(vector1, vector2, 'b', vector4);
            });
        }

        [TestMethod()]
        public void ConcatRealTest() {
            Vector<Pow2.N4> vector1 = Vector<Pow2.N4>.Fill(1, value: -1);
            Vector<Pow2.N4> vector2 = Vector<Pow2.N4>.Fill(2, value: -2);
            Vector<Pow2.N4> vector4 = Vector<Pow2.N4>.Fill(4, value: -3);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-2, -2, -3, -3, -3, -3, -1), ComplexVector<Pow2.N4>.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 1, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 2, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 3, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, (MultiPrecision<Pow2.N4>)3, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, Complex<Pow2.N4>.ImaginaryOne, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, Complex<Pow2.N4>.ImaginaryOne, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 4, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, 5, -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new ComplexVector<Pow2.N4>(-1, -2, -2, "6.2", -3, -3, -3, -3), ComplexVector<Pow2.N4>.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsExactly<ArgumentException>(() => {
                ComplexVector<Pow2.N4>.Concat(vector1, vector2, 'b', vector4);
            });
        }
    }
}