using MultiPrecision;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTest {
    [TestClass]
    public class VectorTest {
        [TestMethod]
        public void CreateTest() {
            ComplexVector<Pow2.N4> v = new(
                (1, 2),
                (-1, 3),
                (3, -4)
            );

            Assert.AreEqual((1, 2), v[0]);
            Assert.AreEqual((-1, 3), v[1]);
            Assert.AreEqual((3, -4), v[2]);
        }

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
        public void DivTest() {
            ComplexVector<Pow2.N4> v1 = TestCase<Pow2.N4>.RandomVector(4);
            ComplexVector<Pow2.N4> v2 = TestCase<Pow2.N4>.RandomVector(4);

            ComplexVector<Pow2.N4> v = v1 / v2;

            Assert.AreEqual(v1[0] / v2[0], v[0]);
            Assert.AreEqual(v1[1] / v2[1], v[1]);
            Assert.AreEqual(v1[2] / v2[2], v[2]);
            Assert.AreEqual(v1[3] / v2[3], v[3]);
        }
    }
}