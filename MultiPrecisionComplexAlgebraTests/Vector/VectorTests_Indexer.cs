using MultiPrecision;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod()]
        public void RangeIndexerGetterTest() {
            ComplexVector<Pow2.N4> vector = new(1, 2, 3, 4, 5);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 4, 5), vector[..]);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(2, 3, 4, 5), vector[1..]);
            Assert.AreEqual(new ComplexVector<Pow2.N4>(3, 4, 5), vector[2..]);
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 4), vector[..^1]);
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 4), vector[..4]);
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3), vector[..^2]);
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3), vector[..3]);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(2, 3, 4), vector[1..4]);
            Assert.AreEqual(new ComplexVector<Pow2.N4>(2, 3, 4), vector[1..^1]);
        }

        [TestMethod()]
        public void RangeIndexerSetterTest() {
            ComplexVector<Pow2.N4> vector_src = new(1, 2, 3, 4, 5);
            ComplexVector<Pow2.N4> vector_dst;

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..] = vector_src;
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 4, 5), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[1..] = vector_src[1..];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(0, 2, 3, 4, 5), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[2..] = vector_src[2..];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(0, 0, 3, 4, 5), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..^1] = vector_src[..^1];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..4] = vector_src[..4];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..^2] = vector_src[..^2];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 0, 0), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..3] = vector_src[..3];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(1, 2, 3, 0, 0), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[1..4] = vector_src[1..4];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(0, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[1..^1] = vector_src[1..^1];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(0, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[0..^2] = vector_src[1..^1];
            Assert.AreEqual(new ComplexVector<Pow2.N4>(2, 3, 4, 0, 0), vector_dst);

            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..^2];
            });

            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..];
            });
        }

        [TestMethod()]
        public void ArrayIndexerTest() {
            ComplexVector<Pow2.N4> v = new(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual(new ComplexVector<Pow2.N4>(5, 2, 3, 7), v[[4, 1, 2, 6]]);

            v[[2, 1, 3]] = new(4, 0, 8);

            Assert.AreEqual(4, v[2]);
            Assert.AreEqual(0, v[1]);
            Assert.AreEqual(8, v[3]);
        }
    }
}