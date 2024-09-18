using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;
using System.Collections;
using System.Diagnostics;
using System.Numerics;

namespace MultiPrecisionComplexAlgebra {
    [DebuggerDisplay("{Convert<MultiPrecision.Pow2.N4>().ToString(),nq}")]
    public partial class ComplexMatrix<N> :
        ICloneable, IFormattable,
        IEnumerable<(int row_index, int column_index, Complex<N> val)>,
        IAdditionOperators<ComplexMatrix<N>, ComplexMatrix<N>, ComplexMatrix<N>>,
        ISubtractionOperators<ComplexMatrix<N>, ComplexMatrix<N>, ComplexMatrix<N>>,
        IMultiplyOperators<ComplexMatrix<N>, ComplexMatrix<N>, ComplexMatrix<N>>,
        IUnaryPlusOperators<ComplexMatrix<N>, ComplexMatrix<N>>,
        IUnaryNegationOperators<ComplexMatrix<N>, ComplexMatrix<N>>
        where N : struct, IConstant {

        internal readonly Complex<N>[,] e;

        protected ComplexMatrix(Complex<N>[,] m, bool cloning) {
            this.e = cloning ? (Complex<N>[,])m.Clone() : m;
        }

        protected ComplexMatrix(int rows, int columns) {
            if (rows <= 0 || columns <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(rows)},{nameof(columns)}");
            }

            this.e = new Complex<N>[rows, columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = Complex<N>.Zero;
                }
            }
        }

        public ComplexMatrix(Complex<N>[,] m) : this(m, cloning: true) { }

        public ComplexMatrix(Matrix<N> real, Matrix<N> imag) {
            if (real.Shape != imag.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(real)}, {nameof(imag)}");
            }

            this.e = new Complex<N>[real.Rows, real.Columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = (real[i, j], imag[i, j]);
                }
            }
        }

        public int Rows => e.GetLength(0);

        public int Columns => e.GetLength(1);

        public (int rows, int columns) Shape => (e.GetLength(0), e.GetLength(1));

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Size {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                return Rows;
            }
        }

        public static implicit operator Complex<N>[,](ComplexMatrix<N> matrix) {
            return (Complex<N>[,])matrix.e.Clone();
        }

        public static implicit operator ComplexMatrix<N>(Complex<N>[,] arr) {
            return new ComplexMatrix<N>(arr);
        }

        public static implicit operator ComplexMatrix<N>(Matrix<N> matrix) {
            Complex<N>[,] ret = new Complex<N>[matrix.Rows, matrix.Columns];

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    ret[i, j] = matrix[i, j];
                }
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix<N> R {
            get {
                MultiPrecision<N>[,] ret = new MultiPrecision<N>[Rows, Columns];

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        ret[i, j] = e[i, j].R;
                    }
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix<N> I {
            get {
                MultiPrecision<N>[,] ret = new MultiPrecision<N>[Rows, Columns];

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        ret[i, j] = e[i, j].I;
                    }
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix<N> Conj => Conjugate(this);

        public static ComplexMatrix<N> Conjugate(ComplexMatrix<N> m) {
            Complex<N>[,] ret = new Complex<N>[m.Rows, m.Columns], e = m.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e[i, j].Conj;
                }
            }

            return new ComplexMatrix<N>(ret, cloning: false);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix<N> T => Transpose(this);

        public static ComplexMatrix<N> Transpose(ComplexMatrix<N> m) {
            ComplexMatrix<N> ret = new(m.Columns, m.Rows);

            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    ret.e[j, i] = m.e[i, j];
                }
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix<N> H => Adjoint(this);

        public static ComplexMatrix<N> Adjoint(ComplexMatrix<N> m) {
            ComplexMatrix<N> ret = new(m.Columns, m.Rows);

            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    ret.e[j, i] = m.e[i, j].Conj;
                }
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix<N> Inverse => Invert(this);

        public static ComplexMatrix<N> Invert(ComplexMatrix<N> m) {
            if (IsZero(m) || !IsFinite(m)) {
                return Invalid(m.Columns, m.Rows);
            }
            if (m.Rows == m.Columns) {
                if (m.Rows == 1) {
                    return new ComplexMatrix<N>(new Complex<N>[,] { { 1 / m.e[0, 0] } }, cloning: false);
                }
                if (m.Rows == 2) {
                    return Invert2x2(m);
                }
                if (m.Rows == 3) {
                    return Invert3x3(m);
                }

                return GaussianEliminate(m);
            }
            else if (m.Rows < m.Columns) {
                ComplexMatrix<N> mh = m.H, mr = m * mh;
                return mh * InversePositiveHermitian(mr, enable_check_hermitian: false);
            }
            else {
                ComplexMatrix<N> mh = m.H, mr = mh * m;
                return InversePositiveHermitian(mr, enable_check_hermitian: false) * mh;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Norm =>
            IsFinite(this) ? (IsZero(this) ? MultiPrecision<N>.Zero : MultiPrecision<N>.Ldexp(MultiPrecision<N>.Sqrt(ScaleB(this, -MaxExponent).SquareNorm), MaxExponent))
            : !IsValid(this) ? MultiPrecision<N>.NaN : MultiPrecision<N>.PositiveInfinity;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> SquareNorm {
            get {
                MultiPrecision<N> sum_sq = MultiPrecision<N>.Zero;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum_sq += e[i, j].Norm;
                    }
                }

                return sum_sq;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Sum {
            get {
                Complex<N> sum = Complex<N>.Zero;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum += e[i, j];
                    }
                }

                return sum;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Mean => Sum / checked(Rows * Columns);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public long MaxExponent {
            get {
                long max_exponent = long.MinValue;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        max_exponent = Math.Max(e[i, j].Exponent, max_exponent);
                    }
                }

                return max_exponent;
            }
        }

        public static ComplexMatrix<N> ScaleB(ComplexMatrix<N> matrix, long n) {
            ComplexMatrix<N> ret = matrix.Copy();

            for (int i = 0; i < ret.Rows; i++) {
                for (int j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = Complex<N>.Ldexp(ret.e[i, j], n);
                }
            }

            return ret;
        }

        public ComplexVector<N> Horizontal(int row_index) {
            ComplexVector<N> ret = ComplexVector<N>.Zero(Columns);

            for (int i = 0; i < Columns; i++) {
                ret.v[i] = e[row_index, i];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector<N>[] Horizontals => (new ComplexVector<N>[Rows]).Select((_, idx) => Horizontal(idx)).ToArray();

        public ComplexVector<N> Vertical(int column_index) {
            ComplexVector<N> ret = ComplexVector<N>.Zero(Rows);

            for (int i = 0; i < Rows; i++) {
                ret.v[i] = e[i, column_index];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector<N>[] Verticals => (new ComplexVector<N>[Columns]).Select((_, idx) => Vertical(idx)).ToArray();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N>[] Diagonals {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                Complex<N>[] diagonals = new Complex<N>[Size];

                for (int i = 0; i < Size; i++) {
                    diagonals[i] = e[i, i];
                }

                return diagonals;
            }
        }

        public static ComplexMatrix<N> FromDiagonals(Complex<N>[] vs) {
            Complex<N>[,] v = new Complex<N>[vs.Length, vs.Length];

            for (int i = 0; i < vs.Length; i++) {
                for (int j = 0; j < vs.Length; j++) {
                    v[i, j] = Complex<N>.Zero;
                }

                v[i, i] = vs[i];
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }

        public static ComplexVector<N> Flatten(ComplexMatrix<N> matrix) {
            Complex<N>[] v = new Complex<N>[matrix.Rows * matrix.Columns];

            for (int i = 0, idx = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++, idx++) {
                    v[idx] = matrix.e[i, j];
                }
            }

            return new ComplexVector<N>(v, cloning: false);
        }

        public static ComplexMatrix<N> Zero(int rows, int columns) {
            return new ComplexMatrix<N>(rows, columns);
        }

        public static ComplexMatrix<N> Zero(int size) {
            return new ComplexMatrix<N>(size, size);
        }

        public static ComplexMatrix<N> Fill(int rows, int columns, Complex<N> value) {
            Complex<N>[,] v = new Complex<N>[rows, columns];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    v[i, j] = value;
                }
            }

            return new ComplexMatrix<N>(v, cloning: false);
        }

        public static ComplexMatrix<N> Fill(int size, MultiPrecision<N> value) {
            return Fill(size, size, value);
        }

        public static ComplexMatrix<N> Identity(int size) {
            ComplexMatrix<N> ret = new(size, size);

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    ret.e[i, j] = (i == j) ? 1 : 0;
                }
            }

            return ret;
        }

        public static ComplexMatrix<N> Invalid(int rows, int columns) {
            return Fill(rows, columns, value: MultiPrecision<N>.NaN);
        }

        public static ComplexMatrix<N> Invalid(int size) {
            return Fill(size, value: MultiPrecision<N>.NaN);
        }

        public static bool IsSquare(ComplexMatrix<N> matrix) {
            return matrix.Rows == matrix.Columns;
        }

        public static bool IsDiagonal(ComplexMatrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i != j && matrix.e[i, j] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsZero(ComplexMatrix<N> matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!Complex<N>.IsZero(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsIdentity(ComplexMatrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i == j) {
                        if (matrix.e[i, j] != MultiPrecision<N>.One) {
                            return false;
                        }
                    }
                    else {
                        if (!Complex<N>.IsZero(matrix.e[i, j])) {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static bool IsSymmetric(ComplexMatrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = i + 1; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i]) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsHermitian(ComplexMatrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = i; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i].Conj) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsFinite(ComplexMatrix<N> matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!Complex<N>.IsFinite(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsInfinity(ComplexMatrix<N> matrix) {
            if (!IsValid(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (MultiPrecision<N>.IsInfinity(matrix.e[i, j].R)) {
                        return true;
                    }
                    if (MultiPrecision<N>.IsInfinity(matrix.e[i, j].I)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsValid(ComplexMatrix<N> matrix) {
            if (matrix.Rows < 1 || matrix.Columns < 1) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (MultiPrecision<N>.IsNaN(matrix.e[i, j].R)) {
                        return false;
                    }
                    if (MultiPrecision<N>.IsNaN(matrix.e[i, j].I)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsRegular(ComplexMatrix<N> matrix) {
            return IsFinite(Invert(matrix));
        }

        public override bool Equals(object obj) {
            return (obj is not null) && obj is ComplexMatrix<N> matrix && matrix == this;
        }

        public override int GetHashCode() {
            return e[0, 0].GetHashCode();
        }

        public object Clone() {
            return new ComplexMatrix<N>(e);
        }

        public ComplexMatrix<N> Copy() {
            return new ComplexMatrix<N>(e);
        }

        public IEnumerator<(int row_index, int column_index, Complex<N> val)> GetEnumerator() {
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    yield return (i, j, e[i, j]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ComplexMatrix<M> Convert<M>() where M : struct, IConstant {
            Complex<M>[,] ret = new Complex<M>[Rows, Columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    ret[i, j] = e[i, j].Convert<M>();
                }
            }

            return ret;
        }
    }
}
