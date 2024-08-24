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

        [TestMethod()]
        public void SampleTest() {
            // solve for v: Av=x
            ComplexMatrix<Pow2.N4> a = new Complex<Pow2.N4>[,] { { (1, 1), (1, 2) }, { (1, 3), (4, -1) } };
            ComplexVector<Pow2.N4> x = ((4, 2), (-1, 3));

            ComplexVector<Pow2.N4> v = ComplexMatrix<Pow2.N4>.Solve(a, x);

            Console.WriteLine(v);
        }

        [TestMethod]
        public void ConjugateTest() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            ComplexMatrix<Pow2.N4> m_conj = m.Conj;

            Assert.AreNotEqual(m, m_conj);

            Assert.AreEqual((1, -2), m_conj[0, 0]);
            Assert.AreEqual((-1, -3), m_conj[0, 1]);
            Assert.AreEqual((3, 4), m_conj[1, 0]);
            Assert.AreEqual((5, 5), m_conj[1, 1]);
            Assert.AreEqual((4, 7), m_conj[2, 0]);
            Assert.AreEqual((2, 3), m_conj[2, 1]);
        }
    }
}