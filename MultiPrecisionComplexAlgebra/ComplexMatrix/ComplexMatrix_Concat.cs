using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static ComplexMatrix<N> Concat(object[,] blocks) {
            if (blocks.GetLength(0) < 1 || blocks.GetLength(1) < 1) {
                throw new ArgumentException("empty array", nameof(blocks));
            }

            (int rows, int columns)[,] sizes = new (int, int)[blocks.GetLength(0), blocks.GetLength(1)];

            for (int i = 0; i < blocks.GetLength(0); i++) {
                for (int j = 0; j < blocks.GetLength(1); j++) {
                    object block = blocks[i, j];

                    if (block is ComplexMatrix<N> cmatrix) {
                        sizes[i, j] = (cmatrix.Rows, cmatrix.Columns);
                    }
                    else if (block is Matrix<N> matrix) {
                        sizes[i, j] = (matrix.Rows, matrix.Columns);
                    }
                    else if (block is ComplexVector<N> cvector) {
                        sizes[i, j] = (cvector.Dim, 1);
                    }
                    else if (block is Vector<N> vector) {
                        sizes[i, j] = (vector.Dim, 1);
                    }
                    else if (block is Complex<N> || block is MultiPrecision<N> || block is double || block is int || block is long || block is float || block is string) {
                        sizes[i, j] = (1, 1);
                    }
                    else {
                        throw new ArgumentException($"unsupported type '{block.GetType().Name}'", nameof(blocks));
                    }
                }
            }

            for (int i = 0; i < blocks.GetLength(0); i++) {
                for (int j = 0; j < blocks.GetLength(1); j++) {
                    if (sizes[i, 0].rows != sizes[i, j].rows || sizes[0, j].columns != sizes[i, j].columns) {
                        throw new ArgumentException("invalid size", $"{nameof(blocks)}[{i}, {j}]");
                    }
                }
            }

            (int start, int end)[] row_ranges = new (int, int)[blocks.GetLength(0)], column_ranges = new (int, int)[blocks.GetLength(1)];

            row_ranges[0] = (0, sizes[0, 0].rows);
            column_ranges[0] = (0, sizes[0, 0].columns);

            for (int i = 1; i < blocks.GetLength(0); i++) {
                row_ranges[i] = (row_ranges[i - 1].end, row_ranges[i - 1].end + sizes[i, 0].rows);
            }

            for (int j = 1; j < blocks.GetLength(1); j++) {
                column_ranges[j] = (column_ranges[j - 1].end, column_ranges[j - 1].end + sizes[0, j].columns);
            }

            ComplexMatrix<N> m = Zero(row_ranges[^1].end, column_ranges[^1].end);

            for (int i = 0; i < blocks.GetLength(0); i++) {
                Range row_range = new(row_ranges[i].start, row_ranges[i].end);

                for (int j = 0; j < blocks.GetLength(1); j++) {
                    Range column_range = new(column_ranges[j].start, column_ranges[j].end);

                    object block = blocks[i, j];

                    if (block is ComplexMatrix<N> cmatrix) {
                        m[row_range, column_range] = cmatrix;
                    }
                    else if (block is Matrix<N> matrix) {
                        m[row_range, column_range] = matrix;
                    }
                    else if (block is ComplexVector<N> cvector) {
                        m[row_range, column_ranges[j].start] = cvector;
                    }
                    else if (block is Vector<N> vector) {
                        m[row_range, column_ranges[j].start] = vector;
                    }
                    else if (block is Complex<N> vmpc) {
                        m[row_ranges[i].start, column_ranges[j].start] = vmpc;
                    }
                    else if (block is MultiPrecision<N> vmp) {
                        m[row_ranges[i].start, column_ranges[j].start] = vmp;
                    }
                    else if (block is System.Numerics.Complex vdc) {
                        m[row_ranges[i].start, column_ranges[j].start] = vdc;
                    }
                    else if (block is double vd) {
                        m[row_ranges[i].start, column_ranges[j].start] = vd;
                    }
                    else if (block is int vi) {
                        m[row_ranges[i].start, column_ranges[j].start] = vi;
                    }
                    else if (block is long vl) {
                        m[row_ranges[i].start, column_ranges[j].start] = vl;
                    }
                    else if (block is float vf) {
                        m[row_ranges[i].start, column_ranges[j].start] = vf;
                    }
                    else if (block is string vs) {
                        m[row_ranges[i].start, column_ranges[j].start] = vs;
                    }
                }
            }

            return m;
        }

        public static ComplexMatrix<N> Concat(ComplexMatrix<N>[,] blocks) {
            if (blocks.GetLength(0) < 1 || blocks.GetLength(1) < 1) {
                throw new ArgumentException("empty array", nameof(blocks));
            }

            (int rows, int columns)[,] sizes = new (int, int)[blocks.GetLength(0), blocks.GetLength(1)];

            for (int i = 0; i < blocks.GetLength(0); i++) {
                for (int j = 0; j < blocks.GetLength(1); j++) {
                    ComplexMatrix<N> matrix = blocks[i, j];

                    sizes[i, j] = (matrix.Rows, matrix.Columns);
                }
            }

            for (int i = 0; i < blocks.GetLength(0); i++) {
                for (int j = 0; j < blocks.GetLength(1); j++) {
                    if (sizes[i, 0].rows != sizes[i, j].rows || sizes[0, j].columns != sizes[i, j].columns) {
                        throw new ArgumentException("invalid size", $"{nameof(blocks)}[{i}, {j}]");
                    }
                }
            }

            (int start, int end)[] row_ranges = new (int, int)[blocks.GetLength(0)], column_ranges = new (int, int)[blocks.GetLength(1)];

            row_ranges[0] = (0, sizes[0, 0].rows);
            column_ranges[0] = (0, sizes[0, 0].columns);

            for (int i = 1; i < blocks.GetLength(0); i++) {
                row_ranges[i] = (row_ranges[i - 1].end, row_ranges[i - 1].end + sizes[i, 0].rows);
            }

            for (int j = 1; j < blocks.GetLength(1); j++) {
                column_ranges[j] = (column_ranges[j - 1].end, column_ranges[j - 1].end + sizes[0, j].columns);
            }

            ComplexMatrix<N> m = Zero(row_ranges[^1].end, column_ranges[^1].end);

            for (int i = 0; i < blocks.GetLength(0); i++) {
                Range row_range = new(row_ranges[i].start, row_ranges[i].end);

                for (int j = 0; j < blocks.GetLength(1); j++) {
                    Range column_range = new(column_ranges[j].start, column_ranges[j].end);

                    ComplexMatrix<N> matrix = blocks[i, j];

                    m[row_range, column_range] = matrix;
                }
            }

            return m;
        }

        public static ComplexMatrix<N> VConcat(params ComplexVector<N>[] vectors) {
            if (vectors.Length < 1) {
                throw new ArgumentException("empty array", nameof(vectors));
            }

            int colomns = vectors[0].Dim;

            if (vectors.Any(v => v.Dim != colomns)) {
                throw new ArgumentException("mismatch size", nameof(vectors));
            }

            ComplexMatrix<N> m = new(vectors.Length, colomns);

            for (int i = 0; i < vectors.Length; i++) {
                m[i, ..] = vectors[i];
            }

            return m;
        }

        public static ComplexMatrix<N> VConcat(params ComplexMatrix<N>[] matrixs) {
            if (matrixs.Length < 1) {
                throw new ArgumentException("empty array", nameof(matrixs));
            }

            int colomns = matrixs[0].Columns;

            if (matrixs.Any(m => m.Columns != colomns)) {
                throw new ArgumentException("mismatch size", nameof(matrixs));
            }

            ComplexMatrix<N> m = new(matrixs.Sum(m => m.Rows), colomns);

            int row_index = 0;
            foreach (ComplexMatrix<N> matrix in matrixs) {
                m[row_index..(row_index + matrix.Rows), ..] = matrix;
                row_index += matrix.Rows;
            }

            return m;
        }

        public static ComplexMatrix<N> HConcat(params ComplexVector<N>[] vectors) {
            if (vectors.Length < 1) {
                throw new ArgumentException("empty array", nameof(vectors));
            }

            int rows = vectors[0].Dim;

            if (vectors.Any(v => v.Dim != rows)) {
                throw new ArgumentException("mismatch size", nameof(vectors));
            }

            ComplexMatrix<N> m = new(rows, vectors.Length);

            for (int i = 0; i < vectors.Length; i++) {
                m[.., i] = vectors[i];
            }

            return m;
        }

        public static ComplexMatrix<N> HConcat(params ComplexMatrix<N>[] matrixs) {
            if (matrixs.Length < 1) {
                throw new ArgumentException("empty array", nameof(matrixs));
            }

            int rows = matrixs[0].Rows;

            if (matrixs.Any(m => m.Rows != rows)) {
                throw new ArgumentException("mismatch size", nameof(matrixs));
            }

            ComplexMatrix<N> m = new(rows, matrixs.Sum(m => m.Columns));

            int col_index = 0;
            foreach (ComplexMatrix<N> matrix in matrixs) {
                m[.., col_index..(col_index + matrix.Columns)] = matrix;
                col_index += matrix.Columns;
            }

            return m;
        }
    }
}