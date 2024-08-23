using MultiPrecision;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTest {
    [TestClass]
    public class MatrixTest {
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
        public void AddTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = TestCase<Pow2.N4>.RandomMatrix(2, 3);

            ComplexMatrix<Pow2.N4> m = m1 + m2;

            Assert.AreEqual(m1[0, 0] + m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] + m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] + m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] + m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] + m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] + m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void SubTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = TestCase<Pow2.N4>.RandomMatrix(2, 3);

            ComplexMatrix<Pow2.N4> m = m1 - m2;

            Assert.AreEqual(m1[0, 0] - m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] - m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] - m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] - m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] - m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] - m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void ElementwiseMulTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = TestCase<Pow2.N4>.RandomMatrix(2, 3);

            ComplexMatrix<Pow2.N4> m = ComplexMatrix<Pow2.N4>.ElementwiseMul(m1, m2);

            Assert.AreEqual(m1[0, 0] * m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] * m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] * m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] * m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] * m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] * m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void ElementwiseDivTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = TestCase<Pow2.N4>.RandomMatrix(2, 3);

            ComplexMatrix<Pow2.N4> m = ComplexMatrix<Pow2.N4>.ElementwiseDiv(m1, m2);

            Assert.AreEqual(m1[0, 0] / m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] / m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] / m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] / m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] / m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] / m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void MatMulTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = TestCase<Pow2.N4>.RandomMatrix(3, 4);

            ComplexMatrix<Pow2.N4> m = m1 * m2;

            Assert.AreEqual(m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0] + m1[0, 2] * m2[2, 0], m[0, 0]);
            Assert.AreEqual(m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0] + m1[1, 2] * m2[2, 0], m[1, 0]);

            Assert.AreEqual(m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1] + m1[0, 2] * m2[2, 1], m[0, 1]);
            Assert.AreEqual(m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1] + m1[1, 2] * m2[2, 1], m[1, 1]);

            Assert.AreEqual(m1[0, 0] * m2[0, 2] + m1[0, 1] * m2[1, 2] + m1[0, 2] * m2[2, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] * m2[0, 2] + m1[1, 1] * m2[1, 2] + m1[1, 2] * m2[2, 2], m[1, 2]);

            Assert.AreEqual(m1[0, 0] * m2[0, 3] + m1[0, 1] * m2[1, 3] + m1[0, 2] * m2[2, 3], m[0, 3]);
            Assert.AreEqual(m1[1, 0] * m2[0, 3] + m1[1, 1] * m2[1, 3] + m1[1, 2] * m2[2, 3], m[1, 3]);
        }

        [TestMethod]
        public void InvertTest() {
            for (int n = 1; n <= 8; n++) {
                for (int i = 0; i < 4; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);
                    ComplexMatrix<Pow2.N4> r = m.Inverse;

                    ComplexMatrix<Pow2.N4> k = m * r;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix<Pow2.N4>.Identity(n)).Det.Norm < 1e-30);
                }
            }
        }

        [TestMethod]
        public void PseudoInvertTest() {
            for (int n = 2; n <= 8; n++) {
                for (int i = 0; i < 4; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n - 1);
                    ComplexMatrix<Pow2.N4> r = m.Inverse;

                    ComplexMatrix<Pow2.N4> k = m * r;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix<Pow2.N4>.Identity(n)).Det.Norm < 1e-30);
                }
            }

            for (int n = 2; n <= 8; n++) {
                for (int i = 0; i < 4; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n - 1, n);
                    ComplexMatrix<Pow2.N4> r = m.Inverse;

                    ComplexMatrix<Pow2.N4> k = r * m;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix<Pow2.N4>.Identity(n)).Det.Norm < 1e-30);
                }
            }
        }
    }
}