using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;
using System.Diagnostics;

namespace MultiPrecisionComplexAlgebra {
    /// <summary>行列クラス</summary>
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static Complex<N>[] EigenValues(ComplexMatrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return [m[0, 0]];
            }
            if (m.Size == 2) {
                return EigenValues2x2(m);
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.DecimalDigits;


            for (int iter = 0; iter < precision_level; iter++) {
                (ComplexMatrix<N> q, ComplexMatrix<N> r) = QR(m);
                m = r * q;
            }

            return m.Diagonals;
        }

        public static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) EigenValueVectors(ComplexMatrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return ([m[0, 0]], [new ComplexVector<N>(1)]);
            }
            if (m.Size == 2) {
                return EigenValueVectors2x2(m);
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.DecimalDigits;

            int n = m.Size, k = n - 1;
            bool[] is_convergenced = new bool[n];
            ComplexVector<N> eigen_values = ComplexVector<N>.Fill(n, 1);
            ComplexVector<N>[] eigen_vectors = Identity(n).Horizontals;

            ComplexMatrix<N> d = m, identity = Identity(n);

            ComplexMatrix<N>[] gs_prev = new ComplexMatrix<N>[n];

            for (int iter_qr = 0; iter_qr < precision_level; iter_qr++) {
                Complex<N>[] mu2x2 = EigenValues2x2(d[^2.., ^2..]);
                Complex<N> mu = (d[n - 1, n - 1] - mu2x2[0]).Norm < (d[n - 1, n - 1] - mu2x2[1]).Norm
                    ? mu2x2[0] : mu2x2[1];

                (ComplexMatrix<N> q, ComplexMatrix<N> r) = QR(d - mu * identity);
                d = r * q + mu * identity;

                eigen_values[..(k + 1)] = d.Diagonals;

                for (int i = 0; i < n; i++) {
                    if (is_convergenced[i]) {
                        continue;
                    }

                    if (iter_qr < precision_level - 1) {
                        ComplexMatrix<N> h = m - eigen_values[i] * identity;
                        ComplexMatrix<N> g = h.Inverse;
                        if (IsFinite(g) && g.Norm < MultiPrecision<N>.Ldexp(h.Norm, MultiPrecision<N>.Bits - 2)) {
                            gs_prev[i] = g;
                            continue;
                        }

                        if (gs_prev[i] is null) {
                            is_convergenced[i] = true;
                            continue;
                        }
                    }

                    ComplexMatrix<N> gp = ScaleB(gs_prev[i], -gs_prev[i].MaxExponent);

                    MultiPrecision<N> norm, norm_prev = MultiPrecision<N>.NaN;
                    ComplexVector<N> x = ComplexVector<N>.Fill(n, 0.125), x_prev = x;
                    x[i] = MultiPrecision<N>.One;

                    for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                        x = (gp * x).Normal;

                        if (MultiPrecision<N>.IsNegative(Vector<N>.Dot(x.R, x_prev.R))) {
                            x = -x;
                        }

                        norm = (x - x_prev).Norm;

                        if (norm.Exponent < -MultiPrecision<N>.Bits ||
                            (norm.Exponent < -MultiPrecision<N>.Bits + 8 && norm >= norm_prev)) {

                            break;
                        }

                        x_prev = x;
                        norm_prev = norm;
                    }

                    eigen_vectors[i] = x;
                    is_convergenced[i] = true;

                    if (i == k) {
                        d = d[..^1, ..^1];
                        k -= 1;
                    }
                }

                if (is_convergenced.All(b => b)) {
                    break;
                }
            }

            return (eigen_values, eigen_vectors);
        }

        private static Complex<N>[] EigenValues2x2(ComplexMatrix<N> m) {
            Debug.Assert(m.Size == 2);

            Complex<N> b = m[0, 0] + m[1, 1], c = m[0, 0] - m[1, 1];

            Complex<N> d = Complex<N>.Sqrt(c * c + 4 * m[0, 1] * m[1, 0]);

            Complex<N> val0 = (b - d) / 2;
            Complex<N> val1 = (b + d) / 2;

            return [val0, val1];
        }

        private static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) EigenValueVectors2x2(ComplexMatrix<N> m) {
            Debug.Assert(m.Size == 2);

            if (!Complex<N>.IsZero(m[1, 0])) {
                Complex<N> b = m[0, 0] + m[1, 1], c = m[0, 0] - m[1, 1];

                Complex<N> d = Complex<N>.Sqrt(c * c + 4 * m[0, 1] * m[1, 0]);

                Complex<N> val0 = (b - d) / 2;
                Complex<N> val1 = (b + d) / 2;

                ComplexVector<N> vec0 = new ComplexVector<N>((c - d) / (2 * m[1, 0]), 1).Normal;
                ComplexVector<N> vec1 = new ComplexVector<N>((c + d) / (2 * m[1, 0]), 1).Normal;

                return (new Complex<N>[] { val0, val1 }, new ComplexVector<N>[] { vec0, vec1 });
            }
            else { 
                Complex<N> val0 = m[0, 0];
                Complex<N> val1 = m[1, 1];

                ComplexVector<N> vec0 = (1, 0);
                ComplexVector<N> vec1 = new ComplexVector<N>(m[0, 1] / (m[1, 1] - m[0, 0]), 1).Normal;

                return (new Complex<N>[] { val0, val1 }, new ComplexVector<N>[] { vec0, vec1 });
            }
        }
    }
}
