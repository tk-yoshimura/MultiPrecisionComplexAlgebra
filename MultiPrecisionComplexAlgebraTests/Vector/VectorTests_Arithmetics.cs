using MultiPrecision;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod]
        public void AddTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(4);
            ComplexVector<Pow2.N4> v2 = TestCase<Pow2.N4>.RandomVector(4);

            ComplexVector<Pow2.N4> v = v1 + v2;

            Assert.AreEqual(v1[0] + v2[0], v[0]);
            Assert.AreEqual(v1[1] + v2[1], v[1]);
            Assert.AreEqual(v1[2] + v2[2], v[2]);
            Assert.AreEqual(v1[3] + v2[3], v[3]);
        }

        [TestMethod]
        public void AddScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexVector<Pow2.N4> v = TestCase<Pow2.N4>.RandomVector(2);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                Assert.AreEqual(v[0] + c, (v + c)[0]);
                Assert.AreEqual(v[0] + r, (v + r)[0]);
                Assert.AreEqual(c + v[0], (c + v)[0]);
                Assert.AreEqual(r + v[0], (r + v)[0]);
            }
        }

        [TestMethod]
        public void SubTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(4);
            ComplexVector<Pow2.N4> v2 = TestCase<Pow2.N4>.RandomVector(4);

            ComplexVector<Pow2.N4> v = v1 - v2;

            Assert.AreEqual(v1[0] - v2[0], v[0]);
            Assert.AreEqual(v1[1] - v2[1], v[1]);
            Assert.AreEqual(v1[2] - v2[2], v[2]);
            Assert.AreEqual(v1[3] - v2[3], v[3]);
        }

        [TestMethod]
        public void SubScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexVector<Pow2.N4> v = TestCase<Pow2.N4>.RandomVector(2);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                Assert.AreEqual(v[0] - c, (v - c)[0]);
                Assert.AreEqual(v[0] - r, (v - r)[0]);
                Assert.AreEqual(c - v[0], (c - v)[0]);
                Assert.AreEqual(r - v[0], (r - v)[0]);
            }
        }

        [TestMethod]
        public void MulTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(4);
            ComplexVector<Pow2.N4> v2 = TestCase<Pow2.N4>.RandomVector(4);

            ComplexVector<Pow2.N4> v = v1 * v2;

            Assert.AreEqual(v1[0] * v2[0], v[0]);
            Assert.AreEqual(v1[1] * v2[1], v[1]);
            Assert.AreEqual(v1[2] * v2[2], v[2]);
            Assert.AreEqual(v1[3] * v2[3], v[3]);
        }

        [TestMethod]
        public void MulScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexVector<Pow2.N4> v = TestCase<Pow2.N4>.RandomVector(2);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                Assert.AreEqual(v[0] * c, (v * c)[0]);
                Assert.AreEqual(v[0] * r, (v * r)[0]);
                Assert.AreEqual(c * v[0], (c * v)[0]);
                Assert.AreEqual(r * v[0], (r * v)[0]);
            }
        }

        [TestMethod]
        public void DivTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(4);
            ComplexVector<Pow2.N4> v2 = TestCase<Pow2.N4>.RandomVector(4);

            ComplexVector<Pow2.N4> v = v1 / v2;

            Assert.AreEqual(v1[0] / v2[0], v[0]);
            Assert.AreEqual(v1[1] / v2[1], v[1]);
            Assert.AreEqual(v1[2] / v2[2], v[2]);
            Assert.AreEqual(v1[3] / v2[3], v[3]);
        }

        [TestMethod]
        public void DivScalarTest() {
            for (int i = 0; i < 16; i++) {
                ComplexVector<Pow2.N4> v = TestCase<Pow2.N4>.RandomVector(2);
                Complex<Pow2.N4> c = TestCase<Pow2.N4>.RandomComplex;
                MultiPrecision<Pow2.N4> r = TestCase<Pow2.N4>.RandomScalar;

                if (r == 0 || c == 0) {
                    continue;
                }

                Assert.IsTrue(((v[0] / c) - ((v / c)[0])).Norm < 1e-30);
                Assert.IsTrue(((v[0] / r) - ((v / r)[0])).Norm < 1e-30);
                Assert.IsTrue(((c / v[0]) - ((c / v)[0])).Norm < 1e-30);
                Assert.IsTrue(((r / v[0]) - ((r / v)[0])).Norm < 1e-30);
            }
        }

        [TestMethod]
        public void UnaryPlusTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(2);
            ComplexVector<Pow2.N4> v2 = +v1;

            Assert.AreEqual(v1[0], v2[0]);
            Assert.AreEqual(v1[1], v2[1]);
        }

        [TestMethod]
        public void UnaryMinusTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(2);
            ComplexVector<Pow2.N4> v2 = -v1;

            Assert.AreEqual(-v1[0], v2[0]);
            Assert.AreEqual(-v1[1], v2[1]);
        }

        [TestMethod]
        public void SumTest() {
            ComplexVector<Pow2.N4> m = TestCase<Pow2.N4>.RandomVector(4);

            Complex<Pow2.N4> v = m.Sum, u = m.Mean;

            Assert.AreEqual(m[0] + m[1] + m[2] + m[3], v);
            Assert.AreEqual((m[0] + m[1] + m[2] + m[3]) / 4, u);
        }
    }
}