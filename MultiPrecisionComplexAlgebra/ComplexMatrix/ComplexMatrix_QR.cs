using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static (ComplexMatrix<N> q, ComplexMatrix<N> r) QR(ComplexMatrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return (Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (Zero(n), Zero(n));
            }

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            ComplexMatrix<N> r = m, q = Identity(n);
            ComplexVector<N> u = ComplexVector<N>.Zero(n);

            for (int k = 0; k < n - 1; k++) {
                MultiPrecision<N> vsum = MultiPrecision<N>.Zero;
                for (int i = k; i < n; i++) {
                    vsum += r.e[i, k].SquareNorm;
                }
                MultiPrecision<N> vnorm = MultiPrecision<N>.Sqrt(vsum);

                if (MultiPrecision<N>.IsZero(vnorm)) {
                    continue;
                }

                Complex<N> x = r.e[k, k];
                u.v[k] = Complex<N>.IsZero(x) ? vnorm : (x + x / x.Norm * vnorm);
                MultiPrecision<N> usum = u.v[k].SquareNorm;

                for (int i = k + 1; i < n; i++) {
                    u.v[i] = r.e[i, k];
                    usum += u.v[i].SquareNorm;
                }
                MultiPrecision<N> c = 2 / usum;

                ComplexMatrix<N> h = Identity(n);
                for (int i = k; i < n; i++) {
                    for (int j = k; j < n; j++) {
                        h.e[i, j] -= c * u[i] * u[j].Conj;
                    }
                }

                r = h * r;
                q *= h;
            }

            for (int i = 0; i < n; i++) {
                if (MultiPrecision<N>.IsNegative(r.e[i, i].R)) {
                    q[.., i] = -q[.., i];
                    r[i, ..] = -r[i, ..];
                }

                for (int k = 0; k < i; k++) {
                    r.e[i, k] = Complex<N>.Zero;
                }

                for (int k = i + 1; k < n; k++) {
                    if (Complex<N>.IsZero(r.e[i, k])) {
                        r.e[i, k] = Complex<N>.Zero;
                    }
                }
            }

            r = ScaleB(r, exponent);

            return (q, r);
        }
    }
}
