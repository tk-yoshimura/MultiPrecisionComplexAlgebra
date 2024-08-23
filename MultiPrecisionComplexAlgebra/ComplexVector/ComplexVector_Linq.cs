using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public static bool Any(ComplexVector<N> vector, Func<Complex<N>, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    return true;
                }
            }

            return false;
        }

        public static bool All(ComplexVector<N> vector, Func<Complex<N>, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!cond(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        public static long Count(ComplexVector<N> vector, Func<Complex<N>, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    cnt++;
                }
            }

            return cnt;
        }
    }
}
