using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> {
        private static (int[] pivot, int pivot_det, ComplexMatrix<N> l, ComplexMatrix<N> u) LUKernel(ComplexMatrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            int[] ps = (new int[n]).Select((_, idx) => idx).ToArray();
            int pivot_det = 1;

            if (!IsFinite(m)) {
                return (ps, 1, Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (ps, 1, Invalid(n), Zero(n));
            }

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            ComplexMatrix<N> l = Zero(n), u = Zero(n);

            for (int i = 0; i < n; i++) {
                MultiPrecision<N> pivot_abs = m.e[i, i].Norm;
                int r = i;

                for (int j = i + 1; j < n; j++) {
                    if (m.e[j, i].Norm > pivot_abs) {
                        pivot_abs = m.e[j, i].Norm;
                        r = j;
                    }
                }

                if (pivot_abs.Exponent <= -MultiPrecision<N>.Bits * 2 + 8) {
                    return (ps, 0, Invalid(n), Zero(n));
                }

                if (r != i) {
                    for (int j = 0; j < n; j++) {
                        (m.e[r, j], m.e[i, j]) = (m.e[i, j], m.e[r, j]);
                    }

                    (ps[r], ps[i]) = (ps[i], ps[r]);

                    pivot_det = -pivot_det;
                }

                for (int j = i + 1; j < n; j++) {
                    Complex<N> mul = m.e[j, i] / m.e[i, i];
                    m.e[j, i] = mul;

                    for (int k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                }
            }

            for (int i = 0; i < n; i++) {
                l.e[i, i] = Complex<N>.One;

                int j = 0;
                for (; j < i; j++) {
                    l.e[i, j] = m.e[i, j];
                }
                for (; j < n; j++) {
                    u.e[i, j] = m.e[i, j];
                }
            }

            u = ScaleB(u, exponent);

            return (ps, pivot_det, l, u);
        }

        public static (Matrix<N> p, ComplexMatrix<N> l, ComplexMatrix<N> u) LU(ComplexMatrix<N> m) {
            (int[] ps, int pivot_det, ComplexMatrix<N> l, ComplexMatrix<N> u) = LUKernel(m);

            int n = m.Size;

            if (pivot_det == 0) {
                return (Matrix<N>.Identity(n), l, u);
            }

            Matrix<N> p = Matrix<N>.Zero(n, n);

            for (int i = 0; i < n; i++) {
                p[i, ps[i]] = MultiPrecision<N>.One;
            }

            return (p, l, u);
        }
    }
}
