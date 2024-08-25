using MultiPrecision;
using MultiPrecisionComplex;
using System.Collections;
using System.Diagnostics;
using System.Numerics;

namespace MultiPrecisionComplexAlgebra {
    [DebuggerDisplay("{Convert<MultiPrecision.Pow2.N4>().ToString(),nq}")]
    public partial class ComplexVector<N> :
        ICloneable, IFormattable,
        IEnumerable<(int index, Complex<N> val)>,
        IAdditionOperators<ComplexVector<N>, ComplexVector<N>, ComplexVector<N>>,
        ISubtractionOperators<ComplexVector<N>, ComplexVector<N>, ComplexVector<N>>,
        IMultiplyOperators<ComplexVector<N>, ComplexVector<N>, ComplexVector<N>>,
        IDivisionOperators<ComplexVector<N>, ComplexVector<N>, ComplexVector<N>>,
        IUnaryPlusOperators<ComplexVector<N>, ComplexVector<N>>,
        IUnaryNegationOperators<ComplexVector<N>, ComplexVector<N>>
        where N : struct, IConstant {

        internal readonly Complex<N>[] v;

        internal ComplexVector(Complex<N>[] v, bool cloning) {
            this.v = cloning ? (Complex<N>[])v.Clone() : v;
        }

        protected ComplexVector(int size) {
            this.v = new Complex<N>[size];

            for (int i = 0; i < v.Length; i++) {
                this.v[i] = Complex<N>.Zero;
            }
        }

        public ComplexVector(params Complex<N>[] v) : this(v, cloning: true) { }

        public ComplexVector(IEnumerable<Complex<N>> v) {
            this.v = v.ToArray();
        }

        public ComplexVector(IReadOnlyCollection<Complex<N>> v) {
            this.v = [.. v];
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> X {
            get => v[0];
            set => v[0] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Y {
            get => v[1];
            set => v[1] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Z {
            get => v[2];
            set => v[2] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> W {
            get => v[3];
            set => v[3] = value;
        }

        public int Dim => v.Length;

        public static implicit operator Complex<N>[](ComplexVector<N> vector) {
            return (Complex<N>[])vector.v.Clone();
        }

        public static implicit operator ComplexVector<N>(Complex<N>[] arr) {
            return new ComplexVector<N>(arr);
        }

        public static implicit operator ComplexVector<N>(MultiPrecisionAlgebra.Vector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                ret[i] = vector[i];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector<N> Conj => Conjugate(this);

        public static ComplexVector<N> Conjugate(ComplexVector<N> v) {
            Complex<N>[] ret = new Complex<N>[v.Dim], e = v.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = Complex<N>.Conjugate(e[i]);
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix<N> Horizontal {
            get {
                ComplexMatrix<N> ret = ComplexMatrix<N>.Zero(1, Dim);

                for (int i = 0; i < Dim; i++) {
                    ret.e[0, i] = v[i];
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix<N> Vertical {
            get {
                ComplexMatrix<N> ret = ComplexMatrix<N>.Zero(Dim, 1);

                for (int i = 0; i < Dim; i++) {
                    ret.e[i, 0] = v[i];
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector<N> Normal => this / Norm;

        public static MultiPrecision<N> Distance(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            return (vector1 - vector2).Norm;
        }

        public static MultiPrecision<N> SquareDistance(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            return (vector1 - vector2).SquareNorm;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Norm =>
            IsFinite(this)
            ? (IsZero(this) ? MultiPrecision<N>.Zero : MultiPrecision<N>.Ldexp(MultiPrecision<N>.Sqrt(ScaleB(this, -MaxExponent).SquareNorm), MaxExponent))
            : !IsValid(this) ? MultiPrecision<N>.NaN : MultiPrecision<N>.PositiveInfinity;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> SquareNorm {
            get {
                MultiPrecision<N> sum_sq = MultiPrecision<N>.Zero;

                foreach (var vi in v) {
                    sum_sq += vi.Norm;
                }

                return sum_sq;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Sum {
            get {
                Complex<N> sum = Complex<N>.Zero;

                for (int i = 0; i < Dim; i++) {
                    sum += v[i];
                }

                return sum;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Mean => Sum / Dim;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public long MaxExponent {
            get {
                long max_exponent = long.MinValue;

                for (int i = 0; i < Dim; i++) {
                    if (MultiPrecision<N>.IsFinite(v[i].R)) {
                        max_exponent = Math.Max(v[i].R.Exponent, max_exponent);
                    }
                    if (MultiPrecision<N>.IsFinite(v[i].I)) {
                        max_exponent = Math.Max(v[i].I.Exponent, max_exponent);
                    }
                }

                return max_exponent;
            }
        }

        public static ComplexVector<N> ScaleB(ComplexVector<N> vector, long n) {
            ComplexVector<N> ret = vector.Copy();

            for (int i = 0; i < ret.Dim; i++) {
                ret.v[i] = (MultiPrecision<N>.Ldexp(ret.v[i].R, n), MultiPrecision<N>.Ldexp(ret.v[i].I, n));
            }

            return ret;
        }

        public static ComplexVector<N> Zero(int size) {
            return new ComplexVector<N>(size);
        }

        public static ComplexVector<N> Fill(int size, Complex<N> value) {
            Complex<N>[] v = new Complex<N>[size];

            for (int i = 0; i < v.Length; i++) {
                v[i] = value;
            }

            return new ComplexVector<N>(v, cloning: false);
        }

        public static ComplexVector<N> Invalid(int size) {
            return Fill(size, value: MultiPrecision<N>.NaN);
        }

        public static bool IsZero(ComplexVector<N> vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!Complex<N>.IsZero(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsFinite(ComplexVector<N> vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!Complex<N>.IsFinite(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsInfinity(ComplexVector<N> vector) {
            if (!IsValid(vector)) {
                return false;
            }

            for (int i = 0; i < vector.Dim; i++) {
                if (MultiPrecision<N>.IsInfinity(vector.v[i].R)) {
                    return true;
                }
                if (MultiPrecision<N>.IsInfinity(vector.v[i].I)) {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValid(ComplexVector<N> vector) {
            if (vector.Dim < 1) {
                return false;
            }

            for (int i = 0; i < vector.Dim; i++) {
                if (MultiPrecision<N>.IsNaN(vector.v[i].R)) {
                    return false;
                }
                if (MultiPrecision<N>.IsNaN(vector.v[i].I)) {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj) {
            return (obj is not null) && obj is ComplexVector<N> vector && vector == this;
        }

        public override int GetHashCode() {
            return Dim > 0 ? v[0].GetHashCode() : 0;
        }

        public object Clone() {
            return new ComplexVector<N>(v);
        }

        public ComplexVector<N> Copy() {
            return new ComplexVector<N>(v);
        }

        public IEnumerator<(int index, Complex<N> val)> GetEnumerator() {
            for (int i = 0; i < Dim; i++) {
                yield return (i, v[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ComplexVector<M> Convert<M>() where M : struct, IConstant {
            Complex<M>[] ret = new Complex<M>[Dim];

            for (int i = 0; i < Dim; i++) {
                ret[i] = v[i].Convert<M>();
            }

            return ret;
        }
    }
}
