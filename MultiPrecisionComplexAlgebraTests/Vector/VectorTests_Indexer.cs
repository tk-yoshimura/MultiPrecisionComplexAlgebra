using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplexAlgebra;
using System;

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

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..^2];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = ComplexVector<Pow2.N4>.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..];
            });
        }
    }
}