using MultiPrecision;
using MultiPrecisionAlgebra;
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
        public void AddScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(2, 3);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                Assert.AreEqual(m[0, 0] + c, (m + c)[0, 0]);
                Assert.AreEqual(m[0, 0] + r, (m + r)[0, 0]);
                Assert.AreEqual(c + m[0, 0], (c + m)[0, 0]);
                Assert.AreEqual(r + m[0, 0], (r + m)[0, 0]);
            }
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
        public void SubScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(2, 3);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                Assert.AreEqual(m[0, 0] - c, (m - c)[0, 0]);
                Assert.AreEqual(m[0, 0] - r, (m - r)[0, 0]);
                Assert.AreEqual(c - m[0, 0], (c - m)[0, 0]);
                Assert.AreEqual(r - m[0, 0], (r - m)[0, 0]);
            }
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
        public void MulScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(2, 3);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                Assert.AreEqual(m[0, 0] * c, (m * c)[0, 0]);
                Assert.AreEqual(m[0, 0] * r, (m * r)[0, 0]);
                Assert.AreEqual(c * m[0, 0], (c * m)[0, 0]);
                Assert.AreEqual(r * m[0, 0], (r * m)[0, 0]);
            }
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
        public void DivScalarTest() {
            for (int i = 0; i < 8; i++) {
                ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(2, 3);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                if (r == 0 || c == 0) {
                    continue;
                }

                Assert.IsTrue(((m[0, 0] / c) - ((m / c)[0, 0])).Norm < 1e-30);
                Assert.IsTrue(((m[0, 0] / r) - ((m / r)[0, 0])).Norm < 1e-30);
                Assert.IsTrue(((c / m[0, 0]) - ((c / m)[0, 0])).Norm < 1e-30);
                Assert.IsTrue(((r / m[0, 0]) - ((r / m)[0, 0])).Norm < 1e-30);
            }
        }

        [TestMethod]
        public void UnaryPlusTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = +m1;

            Assert.AreEqual(m1[0, 0], m2[0, 0]);
            Assert.AreEqual(m1[0, 1], m2[0, 1]);
            Assert.AreEqual(m1[0, 2], m2[0, 2]);
            Assert.AreEqual(m1[1, 0], m2[1, 0]);
            Assert.AreEqual(m1[1, 1], m2[1, 1]);
            Assert.AreEqual(m1[1, 2], m2[1, 2]);
        }

        [TestMethod]
        public void UnaryMinusTest() {
            ComplexMatrix<Pow2.N4> m1 = TestCase<Pow2.N4>.RandomMatrix(2, 3);
            ComplexMatrix<Pow2.N4> m2 = -m1;

            Assert.AreEqual(-m1[0, 0], m2[0, 0]);
            Assert.AreEqual(-m1[0, 1], m2[0, 1]);
            Assert.AreEqual(-m1[0, 2], m2[0, 2]);
            Assert.AreEqual(-m1[1, 0], m2[1, 0]);
            Assert.AreEqual(-m1[1, 1], m2[1, 1]);
            Assert.AreEqual(-m1[1, 2], m2[1, 2]);
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
        public void VectorMulTest() {
            ComplexVector<Pow2.N4> v = TestCase<Pow2.N4>.RandomVector(2);
            ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(2, 3);

            ComplexVector<Pow2.N4> x = v * m;

            Assert.AreEqual(v[0] * m[0, 0] + v[1] * m[1, 0], x[0]);
            Assert.AreEqual(v[0] * m[0, 1] + v[1] * m[1, 1], x[1]);
            Assert.AreEqual(v[0] * m[0, 2] + v[1] * m[1, 2], x[2]);
        }

        [TestMethod]
        public void TraceTest() {
            ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(4, 4);

            Assert.AreEqual(m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3], m.Trace);
        }

        [TestMethod]
        public void InvertTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);

                    if (m.Det.Norm == 0d) {
                        continue;
                    }

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
            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n - 1);
                    ComplexMatrix<Pow2.N4> r = m.Inverse;

                    ComplexMatrix<Pow2.N4> k = m * r;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix<Pow2.N4>.Identity(n)).Det.Norm < 1e-30);
                }
            }

            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n - 1, n);
                    ComplexMatrix<Pow2.N4> r = m.Inverse;

                    ComplexMatrix<Pow2.N4> k = r * m;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix<Pow2.N4>.Identity(n)).Det.Norm < 1e-30);
                }
            }
        }

        [TestMethod]
        public void LUDecompTest() {
            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);

                    (Matrix<Pow2.N4> p, ComplexMatrix<Pow2.N4> l, ComplexMatrix<Pow2.N4> u) =
                        ComplexMatrix<Pow2.N4>.LU(m);

                    ComplexMatrix<Pow2.N4> plu = p * l * u;

                    Console.WriteLine(p);
                    Console.WriteLine(m);
                    Console.WriteLine(l);
                    Console.WriteLine(u);

                    Console.WriteLine(plu);

                    Console.WriteLine((m - plu).Norm);

                    Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs((m - plu).Norm) < 1e-30);
                }
            }
        }

        [TestMethod]
        public void SolveTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> a = TestCase<Pow2.N4>.RandomMatrix(n, n);
                    ComplexVector<Pow2.N4> v = TestCase<Pow2.N4>.RandomVector(n);
                    ComplexVector<Pow2.N4> r = ComplexMatrix<Pow2.N4>.Solve(a, v);

                    ComplexVector<Pow2.N4> x = a.Inverse * v;

                    if (a.Det.Norm == 0d) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine(r);

                    Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs((x - r).Norm) < 1e-30);
                }
            }
        }
    }
}