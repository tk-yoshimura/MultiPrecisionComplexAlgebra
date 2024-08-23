using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static implicit operator ComplexMatrix<N>((Func<Complex<N>, Complex<N>> func, ComplexMatrix<N> arg) sel) {
            return Func(sel.func, sel.arg);
        }

        public static implicit operator ComplexMatrix<N>((Func<Complex<N>, Complex<N>, Complex<N>> func, (ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2);
        }

        public static implicit operator ComplexMatrix<N>((Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>> func, (ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2, ComplexMatrix<N> matrix3) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3);
        }

        public static implicit operator ComplexMatrix<N>((Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>, Complex<N>> func, (ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2, ComplexMatrix<N> matrix3, ComplexMatrix<N> matrix4) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3, sel.args.matrix4);
        }

        public static implicit operator ComplexMatrix<N>((Func<Complex<N>, Complex<N>, Complex<N>> func, ComplexVector<N> vector_row, ComplexVector<N> vector_column) sel) {
            return Map(sel.func, sel.vector_row, sel.vector_column);
        }

        public static ComplexMatrix<N> Func(Func<Complex<N>, Complex<N>> f, ComplexMatrix<N> matrix) {
            Complex<N>[,] x = matrix.e, v = new Complex<N>[matrix.Rows, matrix.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j]);
                }
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }

        public static ComplexMatrix<N> Func(Func<Complex<N>, Complex<N>, Complex<N>> f, ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex<N>[,] x = matrix1.e, y = matrix2.e, v = new Complex<N>[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j]);
                }
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }

        public static ComplexMatrix<N> Func(Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>> f, ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2, ComplexMatrix<N> matrix3) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)}");
            }

            Complex<N>[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, v = new Complex<N>[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j]);
                }
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }

        public static ComplexMatrix<N> Func(Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>, Complex<N>> f, ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2, ComplexMatrix<N> matrix3, ComplexMatrix<N> matrix4) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape || matrix1.Shape != matrix4.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)},{nameof(matrix4)}");
            }

            Complex<N>[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, w = matrix4.e, v = new Complex<N>[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j], w[i, j]);
                }
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }

        public static ComplexMatrix<N> Map(Func<Complex<N>, Complex<N>, Complex<N>> f, ComplexVector<N> vector_row, ComplexVector<N> vector_column) {
            Complex<N>[] row = vector_row.v, col = vector_column.v;
            Complex<N>[,] v = new Complex<N>[row.Length, col.Length];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(row[i], col[j]);
                }
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }
    }
}
