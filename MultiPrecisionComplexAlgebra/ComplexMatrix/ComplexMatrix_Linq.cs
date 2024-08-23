using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static bool Any(ComplexMatrix<N> matrix, Func<Complex<N>, bool> cond) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (cond(matrix.e[i, j])) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool All(ComplexMatrix<N> matrix, Func<Complex<N>, bool> cond) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!cond(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static long Count(ComplexMatrix<N> matrix, Func<Complex<N>, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (cond(matrix.e[i, j])) {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
    }
}
