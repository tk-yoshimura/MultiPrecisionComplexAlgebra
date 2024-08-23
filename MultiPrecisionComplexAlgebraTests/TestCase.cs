using MultiPrecision;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public static class TestCase<N> where N : struct, IConstant {
        static readonly Random random = new(1234);

        public static MultiPrecision<N> RandomScalar {
            get {
                return random.Next(-10, 11);
            }
        }

        public static Complex<N> RandomComplex {
            get {
                return (random.Next(-10, 11), random.Next(-10, 11));
            }
        }

        public static ComplexVector<N> RandomVector(int size) {
            return new ComplexVector<N>((new Complex<N>[size]).Select(_ => RandomComplex));
        }

        public static ComplexMatrix<N> RandomMatrix(int rows, int columns) {
            Complex<N>[,] m = new Complex<N>[rows, columns];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    m[i, j] = RandomComplex;
                }
            }

            return new ComplexMatrix<N>(m);
        }
    }
}
