using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static (ComplexMatrix<N> u, ComplexVector<N> s, ComplexMatrix<N> v) SVD(ComplexMatrix<N> m) {
            if (m.Rows < 1 || m.Columns < 1) {
                throw new ArgumentException("empty matrix", nameof(m));
            }

            if (m.Rows == m.Columns) {
                (ComplexVector<N> eigen_vals, ComplexVector<N>[] eigen_vecs) = EigenValueVectors(m.H * m);

                ComplexMatrix<N> v = HConcat(eigen_vecs);
                ComplexVector<N> s = (Complex<N>.Sqrt, eigen_vals);
                ComplexMatrix<N> u = m * v;

                for (int i = 0; i < s.Dim; i++) {
                    u[.., i] /= s[i];
                }

                return (u, s, v);
            }

            if (m.Rows > m.Columns) {
                (ComplexVector<N> eigen_vals, ComplexVector<N>[] eigen_vecs) = EigenValueVectors(m.H * m);

                ComplexMatrix<N> v = HConcat(eigen_vecs);
                ComplexVector<N> s = (Complex<N>.Sqrt, eigen_vals);
                ComplexMatrix<N> l = m * v;

                for (int i = 0; i < s.Dim; i++) {
                    l[.., i] /= s[i];
                }

                List<ComplexVector<N>> ls = [.. l.Verticals];
                GramSchmidtMethod(ls);

                ComplexMatrix<N> u = HConcat(ls.ToArray());

                return (u, s, v);
            }
            else {
                (ComplexVector<N> eigen_vals, ComplexVector<N>[] eigen_vecs) = EigenValueVectors(m * m.H);

                ComplexMatrix<N> u = HConcat(eigen_vecs);
                ComplexVector<N> s = (Complex<N>.Sqrt, eigen_vals);
                ComplexMatrix<N> r = u.H * m;

                for (int i = 0; i < s.Dim; i++) {
                    r[i, ..] /= s[i];
                }

                List<ComplexVector<N>> rs = [.. r.Horizontals];
                GramSchmidtMethod(rs);

                ComplexMatrix<N> v = HConcat(rs.ToArray()).Conj;

                return (u, s, v);
            }
        }

        private static void GramSchmidtMethod(List<ComplexVector<N>> vs) {
            int n = vs[0].Dim;

            List<ComplexVector<N>> init_vecs = [];
            for (int i = 0; i < n; i++) {
                ComplexVector<N> v = ComplexVector<N>.Zero(n);
                v[i] = 1;
                init_vecs.Add(v);
            }

            while (vs.Count < n) {
                ComplexVector<N> g = init_vecs.OrderBy(u => vs.Select(v => ComplexVector<N>.Dot(u, v).Norm).Sum()).First();

                ComplexVector<N> v = g;

                for (int i = 0; i < vs.Count; i++) {
                    v -= vs[i] * ComplexVector<N>.Dot(g, vs[i]) / vs[i].SquareNorm;
                }

                v = v.Normal;

                vs.Add(v);
            }
        }
    }
}
