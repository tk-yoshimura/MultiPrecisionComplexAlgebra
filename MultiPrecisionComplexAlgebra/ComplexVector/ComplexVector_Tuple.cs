using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public static implicit operator ComplexVector<N>((Complex<N> x, Complex<N> y) v) {
            return new ComplexVector<N>([v.x, v.y], cloning: false);
        }

        public void Deconstruct(out Complex<N> x, out Complex<N> y) {
            if (Dim != 2) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y) = (v[0], v[1]);
        }

        public static implicit operator ComplexVector<N>((Complex<N> x, Complex<N> y, Complex<N> z) v) {
            return new ComplexVector<N>([v.x, v.y, v.z], cloning: false);
        }

        public void Deconstruct(out Complex<N> x, out Complex<N> y, out Complex<N> z) {
            if (Dim != 3) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z) = (v[0], v[1], v[2]);
        }

        public static implicit operator ComplexVector<N>((Complex<N> x, Complex<N> y, Complex<N> z, Complex<N> w) v) {
            return new ComplexVector<N>([v.x, v.y, v.z, v.w], cloning: false);
        }

        public void Deconstruct(out Complex<N> x, out Complex<N> y, out Complex<N> z, out Complex<N> w) {
            if (Dim != 4) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z, w) = (v[0], v[1], v[2], v[3]);
        }

        public static implicit operator ComplexVector<N>((Complex<N> e0, Complex<N> e1, Complex<N> e2, Complex<N> e3, Complex<N> e4) v) {
            return new ComplexVector<N>([v.e0, v.e1, v.e2, v.e3, v.e4], cloning: false);
        }

        public void Deconstruct(out Complex<N> e0, out Complex<N> e1, out Complex<N> e2, out Complex<N> e3, out Complex<N> e4) {
            if (Dim != 5) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4) = (v[0], v[1], v[2], v[3], v[4]);
        }

        public static implicit operator ComplexVector<N>((Complex<N> e0, Complex<N> e1, Complex<N> e2, Complex<N> e3, Complex<N> e4, Complex<N> e5) v) {
            return new ComplexVector<N>([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5], cloning: false);
        }

        public void Deconstruct(out Complex<N> e0, out Complex<N> e1, out Complex<N> e2, out Complex<N> e3, out Complex<N> e4, out Complex<N> e5) {
            if (Dim != 6) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5) = (v[0], v[1], v[2], v[3], v[4], v[5]);
        }

        public static implicit operator ComplexVector<N>((Complex<N> e0, Complex<N> e1, Complex<N> e2, Complex<N> e3, Complex<N> e4, Complex<N> e5, Complex<N> e6) v) {
            return new ComplexVector<N>([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6], cloning: false);
        }

        public void Deconstruct(out Complex<N> e0, out Complex<N> e1, out Complex<N> e2, out Complex<N> e3, out Complex<N> e4, out Complex<N> e5, out Complex<N> e6) {
            if (Dim != 7) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6]);
        }

        public static implicit operator ComplexVector<N>((Complex<N> e0, Complex<N> e1, Complex<N> e2, Complex<N> e3, Complex<N> e4, Complex<N> e5, Complex<N> e6, Complex<N> e7) v) {
            return new ComplexVector<N>([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6, v.e7], cloning: false);
        }

        public void Deconstruct(out Complex<N> e0, out Complex<N> e1, out Complex<N> e2, out Complex<N> e3, out Complex<N> e4, out Complex<N> e5, out Complex<N> e6, out Complex<N> e7) {
            if (Dim != 8) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6, e7) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7]);
        }
    }
}
