using MultiPrecision;
using MultiPrecisionComplex;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static (ComplexMatrix<N> u, ComplexVector<N> s, ComplexMatrix<N> v) SVD(ComplexMatrix<N> m) {
            if (m.Rows == m.Columns) {
                (ComplexVector<N> eigen_vals, ComplexVector<N>[] eigen_vecs) = EigenValueVectors(m * m.H);

                ComplexMatrix<N> u = HConcat(eigen_vecs);
                ComplexVector<N> s = (Complex<N>.Sqrt, eigen_vals);
                ComplexMatrix<N> v = (FromDiagonals(1 / s) * u.H * m).H;

                return (u, s, v);
            }
            
            if (m.Rows > m.Columns) {
                (ComplexVector<N> eigen_vals, ComplexVector<N>[] eigen_vecs) = EigenValueVectors(m * m.H);

                ComplexMatrix<N> u = HConcat(eigen_vecs);
                ComplexVector<N> s = (Complex<N>.Sqrt, eigen_vals);
                ComplexMatrix<N> v = (FromDiagonals(1 / s) * u.H * m).H;

                s = s[..m.Columns];
                v = v[.., ..m.Columns];

                return (u, s, v);
            }
            else { 
                (ComplexVector<N> eigen_vals, ComplexVector<N>[] eigen_vecs) = EigenValueVectors(m.H * m);

                ComplexMatrix<N> v = HConcat(eigen_vecs);
                ComplexVector<N> s = (Complex<N>.Sqrt, eigen_vals);
                ComplexMatrix<N> u = m * v * FromDiagonals(1 / s);

                s = s[..m.Rows];
                u = u[.., ..m.Rows];

                return (u, s, v);
            }
        }
    }
}
