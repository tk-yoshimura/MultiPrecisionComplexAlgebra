using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public static ComplexVector<N> operator +(ComplexVector<N> vector) {
            return (ComplexVector<N>)vector.Clone();
        }

        public static ComplexVector<N> operator -(ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = -v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator +(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex<N>[] ret = new Complex<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] + v2[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator -(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex<N>[] ret = new Complex<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] - v2[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator *(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex<N>[] ret = new Complex<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] * v2[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator /(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex<N>[] ret = new Complex<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] / v2[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator +(MultiPrecision<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r + v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator +(Complex<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r + v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator +(ComplexVector<N> vector, MultiPrecision<N> r) {
            return r + vector;
        }

        public static ComplexVector<N> operator +(ComplexVector<N> vector, Complex<N> r) {
            return r + vector;
        }

        public static ComplexVector<N> operator -(MultiPrecision<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r - v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator -(Complex<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r - v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator -(ComplexVector<N> vector, MultiPrecision<N> r) {
            return (-r) + vector;
        }

        public static ComplexVector<N> operator -(ComplexVector<N> vector, Complex<N> r) {
            return (-r) + vector;
        }

        public static ComplexVector<N> operator *(MultiPrecision<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r * v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator *(Complex<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r * v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator *(ComplexVector<N> vector, MultiPrecision<N> r) {
            return r * vector;
        }

        public static ComplexVector<N> operator *(ComplexVector<N> vector, Complex<N> r) {
            return r * vector;
        }

        public static ComplexVector<N> operator /(MultiPrecision<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r / v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator /(Complex<N> r, ComplexVector<N> vector) {
            Complex<N>[] ret = new Complex<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r / v[i];
            }

            return new ComplexVector<N>(ret, cloning: false);
        }

        public static ComplexVector<N> operator /(ComplexVector<N> vector, MultiPrecision<N> r) {
            return (1 / r) * vector;
        }

        public static ComplexVector<N> operator /(ComplexVector<N> vector, Complex<N> r) {
            return (1 / r) * vector;
        }

        public static Complex<N> Dot(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex<N> sum = Complex<N>.Zero;

            for (int i = 0, dim = vector1.Dim; i < dim; i++) {
                sum += vector1.v[i] * vector2.v[i].Conj;
            }

            return sum;
        }

        public static Complex<N> Polynomial(Complex<N> x, ComplexVector<N> coef) {
            if (coef.Dim < 1) {
                return Complex<N>.Zero;
            }

            Complex<N> y = coef[^1];

            for (int i = coef.Dim - 2; i >= 0; i--) {
                y = coef[i] + x * y;
            }

            return y;
        }

        public static ComplexVector<N> Polynomial(ComplexVector<N> x, ComplexVector<N> coef) {
            if (coef.Dim < 1) {
                return Zero(x.Dim);
            }

            ComplexVector<N> y = Fill(x.Dim, coef[^1]);

            for (int i = coef.Dim - 2; i >= 0; i--) {
                Complex<N> c = coef[i];

                for (int j = 0, n = x.Dim; j < n; j++) {
                    y[j] = c + x[j] * y[j];
                }
            }

            return y;
        }

        public static bool operator ==(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            if (ReferenceEquals(vector1, vector2)) {
                return true;
            }
            if (vector1 is null || vector2 is null) {
                return false;
            }

            return vector1.v.SequenceEqual(vector2.v);
        }

        public static bool operator !=(ComplexVector<N> vector1, ComplexVector<N> vector2) {
            return !(vector1 == vector2);
        }
    }
}
