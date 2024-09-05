using MultiPrecisionComplex;
using MultiPrecision;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static ComplexMatrix<N> Cholesky(ComplexMatrix<N> m, bool enable_check_hermitian = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;
            
            if ((enable_check_hermitian && !IsHermitian(m)) || !IsFinite(m)) {
                return Invalid(n);
            }

            if (IsZero(m)) {
                return Zero(n);
            }

            long exponent = (m.MaxExponent / 2) * 2;

            ComplexMatrix<N> u = ScaleB(m, -exponent);

            Complex<N>[,] v = new Complex<N>[n, n];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < i; j++) {
                    Complex<N> v_ij = u[i, j];
                    for (int k = 0; k < j; k++) {
                        v_ij -= v[i, k] * Complex<N>.Conjugate(v[j, k]);
                    }
                    v[i, j] = v_ij / v[j, j]; 
                }

                Complex<N> v_ii = u[i, i];
                for (int k = 0; k < i; k++) {
                    v_ii -= v[i, k] * Complex<N>.Conjugate(v[i, k]);
                }
                v[i, i] = Complex<N>.Sqrt(v_ii);

                for (int j = i + 1; j < n; j++) {
                    v[i, j] = Complex<N>.Zero;
                }
            }

            ComplexMatrix<N> l = ScaleB(new ComplexMatrix<N>(v, cloning: false), exponent / 2);

            return l;
        }
    }
}
