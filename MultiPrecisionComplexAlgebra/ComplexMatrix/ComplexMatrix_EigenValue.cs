using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;
using System.Diagnostics;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static Complex<N>[] EigenValues(ComplexMatrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return [m[0, 0]];
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValues2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.Length * m.Size * 8;

           int n = m.Size, notconverged = n;
            long exponent = m.MaxExponent;
            ComplexMatrix<N> u = ScaleB(m, -exponent);
            MultiPrecision<N> eps = MultiPrecision<N>.Ldexp(1, -MultiPrecision<N>.Bits + 8);

            ComplexVector<N> eigen_values = ComplexVector<N>.Fill(n, 1);
            ComplexVector<N> eigen_values_prev = eigen_values.Copy();

            Vector<N> eigen_diffnorms = Vector<N>.Fill(n, MultiPrecision<N>.PositiveInfinity);
            Vector<N> eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexMatrix<N> d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex<N>[] mu2x2 = EigenValues2x2(d[^2.., ^2..]);
                    Complex<N> d_kk = d[^1, ^1];
                    Complex<N> mu = (d_kk - mu2x2[0]).Norm < (d_kk - mu2x2[1]).Norm
                        ? mu2x2[0] : mu2x2[1];

                    (ComplexMatrix<N> q, ComplexMatrix<N> r) = QR(DiagonalAdd(d, -mu));
                    d = DiagonalAdd(r * q, mu);

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    MultiPrecision<N> eigen_diffnorm = (eigen_values[i] - eigen_values_prev[i]).Norm;
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (eigen_diffnorms[i].Exponent > -MultiPrecision<N>.Bits + 8 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    ComplexVector<N> lower = d[^1, ..^1];
                    Complex<N> eigen = d[^1, ^1];

                    if (lower.MaxExponent < long.Max(eigen.R.Exponent, eigen.I.Exponent) - MultiPrecision<N>.Bits) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = ComplexVector<N>.ScaleB(eigen_values, exponent);

            return SortEigenByNorm(eigen_values);
        }

        public static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) EigenValueVectors(ComplexMatrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return ([m[0, 0]], [new ComplexVector<N>(1)]);
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValueVectors2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.Length * m.Size * 8;

            int n = m.Size, notconverged = n;
            long exponent = m.MaxExponent;
            ComplexMatrix<N> u = ScaleB(m, -exponent);
            MultiPrecision<N> eps = MultiPrecision<N>.Ldexp(1, -MultiPrecision<N>.Bits + 8);

            ComplexVector<N> eigen_values = ComplexVector<N>.Fill(n, 1);
            ComplexVector<N> eigen_values_prev = eigen_values.Copy();

            Vector<N> eigen_diffnorms = Vector<N>.Fill(n, MultiPrecision<N>.PositiveInfinity);
            Vector<N> eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexVector<N>[] eigen_vectors = Identity(n).Horizontals;

            ComplexMatrix<N> d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex<N>[] mu2x2 = EigenValues2x2(d[^2.., ^2..]);
                    Complex<N> d_kk = d[^1, ^1];
                    Complex<N> mu = (d_kk - mu2x2[0]).Norm < (d_kk - mu2x2[1]).Norm
                        ? mu2x2[0] : mu2x2[1];

                    (ComplexMatrix<N> q, ComplexMatrix<N> r) = QR(DiagonalAdd(d, -mu));
                    d = DiagonalAdd(r * q, mu);

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    MultiPrecision<N> eigen_diffnorm = (eigen_values[i] - eigen_values_prev[i]).Norm;
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (eigen_diffnorms[i].Exponent > -MultiPrecision<N>.Bits + 8 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    Complex<N> eigen_val = eigen_values[i];
                    ComplexMatrix<N> g = DiagonalAdd(u, -eigen_val + eps).Inverse;

                    MultiPrecision<N> norm, norm_prev = MultiPrecision<N>.NaN;
                    ComplexVector<N> x = ComplexVector<N>.Fill(n, 0.125);
                    x[i] = Complex<N>.One;

                    for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                        x = (g * x).Normal;

                        norm = (u * x - eigen_val * x).Norm;

                        if (norm.Exponent < -MultiPrecision<N>.Bits / 2 && norm >= norm_prev) {
                            break;
                        }

                        norm_prev = norm;
                    }

                    eigen_vectors[i] = x;
                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    ComplexVector<N> lower = d[^1, ..^1];
                    Complex<N> eigen = d[^1, ^1];

                    if (lower.MaxExponent < long.Max(eigen.R.Exponent, eigen.I.Exponent) - MultiPrecision<N>.Bits) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = ComplexVector<N>.ScaleB(eigen_values, exponent);

            return SortEigenByNorm((eigen_values, eigen_vectors));
        }

        private static Complex<N>[] EigenValues2x2(ComplexMatrix<N> m) {
            Debug.Assert(m.Size == 2);

            Complex<N> b = m[0, 0] + m[1, 1], c = m[0, 0] - m[1, 1];

            Complex<N> d = Complex<N>.Sqrt(c * c + 4 * m[0, 1] * m[1, 0]);

            Complex<N> val0 = (b + d) / 2;
            Complex<N> val1 = (b - d) / 2;

            return [val0, val1];
        }

        private static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) EigenValueVectors2x2(ComplexMatrix<N> m) {
            Debug.Assert(m.Size == 2);

            long diagonal_scale = long.Max(
                long.Max(m[0, 0].R.Exponent, m[0, 0].I.Exponent),
                long.Max(m[1, 1].R.Exponent, m[1, 1].I.Exponent)
            );

            long m10_scale = long.Max(m[1, 0].R.Exponent, m[1, 0].I.Exponent);

            if (diagonal_scale - m10_scale < MultiPrecision<N>.Bits) {
                Complex<N> b = m[0, 0] + m[1, 1], c = m[0, 0] - m[1, 1];

                Complex<N> d = Complex<N>.Sqrt(c * c + 4 * m[0, 1] * m[1, 0]);

                Complex<N> val0 = (b + d) / 2;
                Complex<N> val1 = (b - d) / 2;

                ComplexVector<N> vec0 = new ComplexVector<N>((c + d) / (2 * m[1, 0]), 1).Normal;
                ComplexVector<N> vec1 = new ComplexVector<N>((c - d) / (2 * m[1, 0]), 1).Normal;

                return (new Complex<N>[] { val0, val1 }, new ComplexVector<N>[] { vec0, vec1 });
            }
            else {
                Complex<N> val0 = m[0, 0];
                Complex<N> val1 = m[1, 1];

                if (val0 != val1) {
                    ComplexVector<N> vec0 = (1, 0);
                    ComplexVector<N> vec1 = new ComplexVector<N>(m[0, 1] / (val1 - val0), 1).Normal;

                    return (new Complex<N>[] { val0, val1 }, new ComplexVector<N>[] { vec0, vec1 });
                }
                else { 
                    return (new Complex<N>[] { val0, val1 }, new ComplexVector<N>[] { (1, 0), (0, 1) });
                }
            }
        }

        private static Complex<N>[] SortEigenByNorm(Complex<N>[] eigen_values) {
            Complex<N>[] eigen_values_sorted = eigen_values.OrderBy(item => item.Norm).ToArray();

            return eigen_values_sorted;
        }

        private static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) SortEigenByNorm((Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) eigens) {
            Debug.Assert(eigens.eigen_values.Length == eigens.eigen_vectors.Length);

            IOrderedEnumerable<(Complex<N> val, ComplexVector<N> vec)> eigens_sorted = 
                eigens.eigen_values.Zip(eigens.eigen_vectors).OrderBy(item => item.First.Norm);

            Complex<N>[] eigen_values_sorted = eigens_sorted.Select(item => item.val).ToArray();
            ComplexVector<N>[] eigen_vectors_sorted = eigens_sorted.Select(item => item.vec).ToArray();

            return (eigen_values_sorted, eigen_vectors_sorted);
        }
    }
}
