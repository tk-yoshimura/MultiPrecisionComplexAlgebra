using MultiPrecision;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    [TestClass]
    public partial class ComplexVectorTests {
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
        public void CopyTest() {
            ComplexVector<Pow2.N4> v1 = new(
                (1, 2),
                (-1, 3),
                (3, -4)
            );

            ComplexVector<Pow2.N4> v2 = v1.Copy();

            Assert.AreEqual(v1, v1);
#pragma warning disable CS1718
            Assert.IsTrue(v1 == v1);
#pragma warning restore CS1718
            Assert.AreEqual(v1, v2);
            Assert.IsTrue(v1 == v2);

            v2[0] = (1, 3);

            Assert.AreNotEqual(v1, v2);
            Assert.IsTrue(v1 != v2);
        }
    }
}