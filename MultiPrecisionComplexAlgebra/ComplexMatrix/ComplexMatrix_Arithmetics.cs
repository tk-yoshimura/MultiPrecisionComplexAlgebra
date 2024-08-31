using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static ComplexMatrix<N> operator +(ComplexMatrix<N> matrix) {
            return matrix.Copy();
        }

        public static ComplexMatrix<N> operator -(ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = -e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator +(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex<N>[,] ret = new Complex<N>[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] + e2[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator -(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex<N>[,] ret = new Complex<N>[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] - e2[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> ElementwiseMul(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex<N>[,] ret = new Complex<N>[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] * e2[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> ElementwiseDiv(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex<N>[,] ret = new Complex<N>[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] / e2[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator *(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (matrix1.Columns != matrix2.Rows) {
                throw new ArgumentException($"mismatch {nameof(matrix1.Columns)} {nameof(matrix2.Rows)}", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex<N>[,] ret = new Complex<N>[matrix1.Rows, matrix2.Columns], e1 = matrix1.e, e2 = matrix2.T.e;

            for (int i = 0, c = matrix1.Columns; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    Complex<N> s = Complex<N>.Zero;

                    for (int k = 0; k < c; k++) {
                        s += e1[i, k] * e2[j, k];
                    }

                    ret[i, j] = s;
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator *(ComplexMatrix<N> matrix, ComplexVector<N> vector) {
            if (matrix.Columns != vector.Dim) {
                throw new ArgumentException($"mismatch {nameof(matrix.Columns)} {nameof(vector.Dim)}", $"{nameof(matrix)},{nameof(vector)}");
            }

            Complex<N>[] ret = new Complex<N>[matrix.Rows], v = vector.v;
            Complex<N>[,] e = matrix.e;

            for (int i = 0; i < matrix.Rows; i++) {
                Complex<N> s = Complex<N>.Zero;

                for (int j = 0; j < matrix.Columns; j++) {
                    s += e[i, j] * v[j];
                }

                ret[i] = s;
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator *(ComplexVector<N> vector, ComplexMatrix<N> matrix) {
            if (vector.Dim != matrix.Rows) {
                throw new ArgumentException($"mismatch {nameof(vector.Dim)} {nameof(matrix.Rows)}", $"{nameof(vector)},{nameof(matrix)}");
            }

            Complex<N>[] ret = new Complex<N>[matrix.Columns], v = vector.v;
            Complex<N>[,] e = matrix.T.e;

            for (int j = 0; j < matrix.Columns; j++) {
                Complex<N> s = Complex<N>.Zero;

                for (int i = 0; i < matrix.Rows; i++) {
                    s += v[i] * e[j, i];
                }

                ret[j] = s;
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator +(MultiPrecision<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r + e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator +(Complex<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r + e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator +(ComplexMatrix<N> matrix, MultiPrecision<N> r) {
            return r + matrix;
        }

        public static ComplexMatrix<N> operator +(ComplexMatrix<N> matrix, Complex<N> r) {
            return r + matrix;
        }

        public static ComplexMatrix<N> operator -(MultiPrecision<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r - e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator -(Complex<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r - e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator -(ComplexMatrix<N> matrix, MultiPrecision<N> r) {
            return (-r) + matrix;
        }

        public static ComplexMatrix<N> operator -(ComplexMatrix<N> matrix, Complex<N> r) {
            return (-r) + matrix;
        }

        public static ComplexMatrix<N> operator *(MultiPrecision<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r * e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator *(Complex<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r * e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator *(ComplexMatrix<N> matrix, MultiPrecision<N> r) {
            return r * matrix;
        }

        public static ComplexMatrix<N> operator *(ComplexMatrix<N> matrix, Complex<N> r) {
            return r * matrix;
        }

        public static ComplexMatrix<N> operator /(MultiPrecision<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r / e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator /(Complex<N> r, ComplexMatrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r / e[i, j];
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        public static ComplexMatrix<N> operator /(ComplexMatrix<N> matrix, MultiPrecision<N> r) {
            return (1 / r) * matrix;
        }

        public static ComplexMatrix<N> operator /(ComplexMatrix<N> matrix, Complex<N> r) {
            return (1 / r) * matrix;
        }

        public static ComplexMatrix<N> DiagonalAdd(ComplexMatrix<N> matrix, Complex<N> c) {
            int n = int.Min(matrix.Rows, matrix.Columns);
            
            ComplexMatrix<N> ret = matrix.Copy();

            for (int i = 0; i < n; i++) {
                ret[i, i] += c;
            }

            return ret;
        }

        public static bool operator ==(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            if (ReferenceEquals(matrix1, matrix2)) {
                return true;
            }
            if (matrix1 is null || matrix2 is null) {
                return false;
            }

            if (matrix1.Shape != matrix2.Shape) {
                return false;
            }

            for (int i = 0; i < matrix1.Rows; i++) {
                for (int j = 0; j < matrix2.Columns; j++) {
                    if (matrix1.e[i, j] != matrix2.e[i, j]) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(ComplexMatrix<N> matrix1, ComplexMatrix<N> matrix2) {
            return !(matrix1 == matrix2);
        }
    }
}
