using MultiPrecision;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void CholeskyTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);
                    ComplexMatrix<Pow2.N4> matrix = m * m.H;

                    Console.WriteLine($"test: {matrix}");

                    ComplexMatrix<Pow2.N4> l = ComplexMatrix<Pow2.N4>.Cholesky(matrix);
                    ComplexMatrix<Pow2.N4> v = l * l.H;

                    Assert.IsTrue((matrix - v).Norm < 1e-28);
                }
            }
        }
    }
}