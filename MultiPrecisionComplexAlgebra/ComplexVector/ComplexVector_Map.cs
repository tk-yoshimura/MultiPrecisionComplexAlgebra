using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public static implicit operator ComplexVector<N>((Func<Complex<N>, Complex<N>> func, ComplexVector<N> arg) sel) {
            return Func(sel.func, sel.arg);
        }

        public static implicit operator ComplexVector<N>((Func<Complex<N>, Complex<N>, Complex<N>> func, (ComplexVector<N> vector1, ComplexVector<N> vector2) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2);
        }

        public static implicit operator ComplexVector<N>((Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>> func, (ComplexVector<N> vector1, ComplexVector<N> vector2, ComplexVector<N> vector3) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3);
        }

        public static implicit operator ComplexVector<N>((Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>, Complex<N>> func, (ComplexVector<N> vector1, ComplexVector<N> vector2, ComplexVector<N> vector3, ComplexVector<N> vector4) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3, sel.args.vector4);
        }

        public static ComplexVector<N> Func(Func<Complex<N>, Complex<N>> f, ComplexVector<N> vector) {
            Complex<N>[] x = vector.v, v = new Complex<N>[vector.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i]);
            }

            return new ComplexVector<N>(v, cloning: false);
        }

        public static ComplexVector<N> Func(Func<Complex<N>, Complex<N>, Complex<N>> f, ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex<N>[] x = vector1.v, y = vector2.v, v = new Complex<N>[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i]);
            }

            return new ComplexVector<N>(v, cloning: false);
        }

        public static ComplexVector<N> Func(Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>> f, ComplexVector<N> vector1, ComplexVector<N> vector2, ComplexVector<N> vector3) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)}");
            }

            Complex<N>[] x = vector1.v, y = vector2.v, z = vector3.v, v = new Complex<N>[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i]);
            }

            return new ComplexVector<N>(v, cloning: false);
        }

        public static ComplexVector<N> Func(Func<Complex<N>, Complex<N>, Complex<N>, Complex<N>, Complex<N>> f, ComplexVector<N> vector1, ComplexVector<N> vector2, ComplexVector<N> vector3, ComplexVector<N> vector4) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim || vector1.Dim != vector4.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)},{nameof(vector4)}");
            }

            Complex<N>[] x = vector1.v, y = vector2.v, z = vector3.v, w = vector4.v, v = new Complex<N>[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i], w[i]);
            }

            return new ComplexVector<N>(v, cloning: false);
        }
    }
}
