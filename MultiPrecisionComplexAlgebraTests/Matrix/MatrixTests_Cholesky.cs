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

                    Assert.IsLessThan(1e-28, (matrix - v).Norm);
                }
            }
        }

        [TestMethod()]
        public void InversePositiveHermitianTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);
                    ComplexMatrix<Pow2.N4> matrix = m * m.H;
                    Console.WriteLine($"test: {matrix}");

                    ComplexMatrix<Pow2.N4> r = ComplexMatrix<Pow2.N4>.InversePositiveHermitian(matrix);

                    Assert.IsLessThan(1e-25, (matrix * r - ComplexMatrix<Pow2.N4>.Identity(matrix.Size)).Norm);
                }
            }
        }

        [TestMethod()]
        public void SlovePositiveHermitianTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);
                    ComplexMatrix<Pow2.N4> matrix = m * m.H;

                    Console.WriteLine($"test: {matrix}");

                    ComplexVector<Pow2.N4> v = ComplexVector<Pow2.N4>.Zero(m.Size);
                    for (int j = 0; j < v.Dim; j++) {
                        v[j] = (j + 2, -j + 3);
                    }

                    ComplexMatrix<Pow2.N4> r = ComplexMatrix<Pow2.N4>.InversePositiveHermitian(matrix);

                    ComplexVector<Pow2.N4> u = ComplexMatrix<Pow2.N4>.SolvePositiveHermitian(matrix, v);
                    ComplexVector<Pow2.N4> t = r * v;

                    Assert.IsLessThan(1e-25, (t - u).Norm);
                }
            }
        }
    }
}