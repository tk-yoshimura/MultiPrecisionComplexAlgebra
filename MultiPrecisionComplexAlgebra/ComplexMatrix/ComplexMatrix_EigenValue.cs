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

            precision_level = precision_level >= 0 ? precision_level : 8 * MultiPrecision<N>.Length * m.Size;

            int n = m.Size, notconverged = n;
            long exponent = m.MaxExponent;
            ComplexMatrix<N> u = ScaleB(m, -exponent);

            ComplexVector<N> eigen_values = ComplexVector<N>.Fill(n, 1);
            ComplexVector<N> eigen_values_prev = eigen_values.Copy();

            Vector<N> eigen_diffnorms = Vector<N>.Fill(n, MultiPrecision<N>.PositiveInfinity);
            Vector<N> eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexMatrix<N> d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex<N> mu = EigenValues2x2(d[^2.., ^2..])[1];

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

            precision_level = precision_level >= 0 ? precision_level : 8 * MultiPrecision<N>.Length * m.Size;

            int n = m.Size, notconverged = n;
            long exponent = m.MaxExponent;
            ComplexMatrix<N> u = ScaleB(m, -exponent);

            ComplexVector<N> diagonal = u.Diagonals;
            bool[] diagonal_sampled = new bool[n];

            ComplexVector<N> eigen_values = ComplexVector<N>.Fill(n, 1);
            ComplexVector<N> eigen_values_prev = eigen_values.Copy();

            Vector<N> eigen_diffnorms = Vector<N>.Fill(n, MultiPrecision<N>.PositiveInfinity);
            Vector<N> eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexVector<N>[] eigen_vectors = Identity(n).Horizontals;

            ComplexMatrix<N> d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex<N> mu = EigenValues2x2(d[^2.., ^2..])[1];

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

                    int nearest_diagonal_index = eigen_val == diagonal[i] && !diagonal_sampled[i]
                        ? i
                        : diagonal
                            .Where(v => !diagonal_sampled[v.index])
                            .OrderBy(v => (v.val - eigen_val).Norm)
                            .First().index;

                    diagonal_sampled[nearest_diagonal_index] = true;

                    ComplexVector<N> v = u[.., nearest_diagonal_index], h = u[nearest_diagonal_index, ..];
                    MultiPrecision<N> nondiagonal_absmax = MultiPrecision<N>.Zero;
                    for (int k = 0; k < v.Dim; k++) {
                        if (k == nearest_diagonal_index) {
                            continue;
                        }

                        nondiagonal_absmax =
                            MultiPrecision<N>.Max(MultiPrecision<N>.Max(
                                nondiagonal_absmax, MultiPrecision<N>.Abs(v[k].R)), MultiPrecision<N>.Abs(h[k].R)
                            );
                        nondiagonal_absmax =
                            MultiPrecision<N>.Max(MultiPrecision<N>.Max(
                                nondiagonal_absmax, MultiPrecision<N>.Abs(v[k].I)), MultiPrecision<N>.Abs(h[k].I)
                            );
                    }

                    MultiPrecision<N> eps = MultiPrecision<N>.Ldexp(nondiagonal_absmax, -MultiPrecision<N>.Bits + 32);

                    ComplexMatrix<N> g = DiagonalAdd(u, -eigen_val + eps).Inverse;

                    ComplexVector<N> x;

                    if (IsFinite(g)) {
                        MultiPrecision<N> norm, norm_prev = MultiPrecision<N>.NaN;
                        x = ComplexVector<N>.Fill(n, 0.125);
                        x[nearest_diagonal_index] = MultiPrecision<N>.One;

                        for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                            x = (g * x).Normal;

                            norm = (u * x - eigen_val * x).Norm;

                            if (norm.Exponent < -MultiPrecision<N>.Bits / 2 && norm >= norm_prev) {
                                break;
                            }

                            norm_prev = norm;
                        }
                    }
                    else {
                        x = Vector<N>.Zero(n);
                        x[nearest_diagonal_index] = 1d;
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

            Complex<N> m00 = m[0, 0], m11 = m[1, 1];
            Complex<N> m01 = m[0, 1], m10 = m[1, 0];

            Complex<N> b = m00 + m11, c = m00 - m11;

            Complex<N> d = Complex<N>.Sqrt(c * c + 4 * m01 * m10);

            Complex<N> val0 = (b + d) / 2;
            Complex<N> val1 = (b - d) / 2;

            if ((val0 - m11).Norm >= (val1 - m11).Norm) {
                return [val0, val1];
            }
            else {
                return [val1, val0];
            }
        }

        private static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) EigenValueVectors2x2(ComplexMatrix<N> m) {
            Debug.Assert(m.Size == 2);

            Complex<N> m00 = m[0, 0], m11 = m[1, 1];
            Complex<N> m01 = m[0, 1], m10 = m[1, 0];

            long diagonal_scale = long.Max(
                long.Max(m00.R.Exponent, m00.I.Exponent),
                long.Max(m11.R.Exponent, m11.I.Exponent)
            );

            long m10_scale = long.Max(m10.R.Exponent, m10.I.Exponent);

            if (diagonal_scale - m10_scale < MultiPrecision<N>.Bits) {
                Complex<N> b = m00 + m11, c = m00 - m11;

                Complex<N> d = Complex<N>.Sqrt(c * c + 4 * m01 * m10);

                Complex<N> val0 = (b + d) / 2;
                Complex<N> val1 = (b - d) / 2;

                ComplexVector<N> vec0 = new ComplexVector<N>((c + d) / (2 * m10), 1).Normal;
                ComplexVector<N> vec1 = new ComplexVector<N>((c - d) / (2 * m10), 1).Normal;

                if ((val0 - m11).Norm >= (val1 - m11).Norm) {
                    return (new Complex<N>[] { val0, val1 }, new ComplexVector<N>[] { vec0, vec1 });
                }
                else {
                    return (new Complex<N>[] { val1, val0 }, new ComplexVector<N>[] { vec1, vec0 });
                }
            }
            else {
                if (m00 != m11) {
                    ComplexVector<N> vec0 = (1, 0);
                    ComplexVector<N> vec1 = new ComplexVector<N>(m01 / (m11 - m00), 1).Normal;

                    return (new Complex<N>[] { m00, m11 }, new ComplexVector<N>[] { vec0, vec1 });
                }
                else {
                    return (new Complex<N>[] { m00, m11 }, new ComplexVector<N>[] { (1, 0), (0, 1) });
                }
            }
        }

        private static Complex<N>[] SortEigenByNorm(Complex<N>[] eigen_values) {
            Complex<N>[] eigen_values_sorted = [.. eigen_values.OrderByDescending(item => item.Norm)];

            return eigen_values_sorted;
        }

        private static (Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) SortEigenByNorm((Complex<N>[] eigen_values, ComplexVector<N>[] eigen_vectors) eigens) {
            Debug.Assert(eigens.eigen_values.Length == eigens.eigen_vectors.Length);

            IOrderedEnumerable<(Complex<N> val, ComplexVector<N> vec)> eigens_sorted =
                eigens.eigen_values.Zip(eigens.eigen_vectors).OrderByDescending(item => item.First.Norm);

            Complex<N>[] eigen_values_sorted = eigens_sorted.Select(item => item.val).ToArray();
            ComplexVector<N>[] eigen_vectors_sorted = eigens_sorted.Select(item => item.vec).ToArray();

            return (eigen_values_sorted, eigen_vectors_sorted);
        }
    }
}
