using MultiPrecision;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    [TestClass]
    public partial class ComplexMatrixTests {
        [TestMethod]
        public void CreateTest() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            Assert.AreEqual((1, 2), m[0, 0]);
            Assert.AreEqual((-1, 3), m[0, 1]);
            Assert.AreEqual((3, -4), m[1, 0]);
            Assert.AreEqual((5, -5), m[1, 1]);
            Assert.AreEqual((4, -7), m[2, 0]);
            Assert.AreEqual((2, -3), m[2, 1]);
        }

        [TestMethod]
        public void CopyTest() {
            ComplexMatrix<Pow2.N4> m1 = new(new Complex<Pow2.N4>[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            ComplexMatrix<Pow2.N4> m2 = m1.Copy();

            Assert.AreEqual(m1, m1);
#pragma warning disable CS1718
            Assert.IsTrue(m1 == m1);
#pragma warning restore CS1718
            Assert.AreEqual(m1, m2);
            Assert.IsTrue(m1 == m2);

            m2[0, 0] = (1, 3);

            Assert.AreNotEqual(m1, m2);
            Assert.IsTrue(m1 != m2);
        }
    }
}