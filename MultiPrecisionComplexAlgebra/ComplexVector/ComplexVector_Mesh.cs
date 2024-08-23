using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public static (ComplexVector<N> x, ComplexVector<N> y) MeshGrid(ComplexVector<N> x, ComplexVector<N> y) {
            int n = checked(x.Dim * y.Dim);

            Complex<N>[] rx = x.v, ry = y.v;
            Complex<N>[] vx = new Complex<N>[n], vy = new Complex<N>[n];

            for (int i = 0, idx = 0; i < y.Dim; i++) {
                for (int j = 0; j < x.Dim; j++, idx++) {
                    vx[idx] = rx[j];
                    vy[idx] = ry[i];
                }
            }

            return (
                new ComplexVector<N>(vx, cloning: false),
                new ComplexVector<N>(vy, cloning: false)
            );
        }

        public static (ComplexVector<N> x, ComplexVector<N> y, ComplexVector<N> z) MeshGrid(ComplexVector<N> x, ComplexVector<N> y, ComplexVector<N> z) {
            int n = checked(x.Dim * y.Dim * z.Dim);

            Complex<N>[] rx = x.v, ry = y.v, rz = z.v;
            Complex<N>[] vx = new Complex<N>[n], vy = new Complex<N>[n], vz = new Complex<N>[n];

            for (int i = 0, idx = 0; i < z.Dim; i++) {
                for (int j = 0; j < y.Dim; j++) {
                    for (int k = 0; k < x.Dim; k++, idx++) {
                        vx[idx] = rx[k];
                        vy[idx] = ry[j];
                        vz[idx] = rz[i];
                    }
                }
            }

            return (
                new ComplexVector<N>(vx, cloning: false),
                new ComplexVector<N>(vy, cloning: false),
                new ComplexVector<N>(vz, cloning: false)
            );
        }

        public static (ComplexVector<N> x, ComplexVector<N> y, ComplexVector<N> z, ComplexVector<N> w) MeshGrid(ComplexVector<N> x, ComplexVector<N> y, ComplexVector<N> z, ComplexVector<N> w) {
            int n = checked(x.Dim * y.Dim * z.Dim * w.Dim);

            Complex<N>[] rx = x.v, ry = y.v, rz = z.v, rw = w.v;
            Complex<N>[] vx = new Complex<N>[n], vy = new Complex<N>[n], vz = new Complex<N>[n], vw = new Complex<N>[n];

            for (int i = 0, idx = 0; i < w.Dim; i++) {
                for (int j = 0; j < z.Dim; j++) {
                    for (int k = 0; k < y.Dim; k++) {
                        for (int m = 0; m < x.Dim; m++, idx++) {
                            vx[idx] = rx[m];
                            vy[idx] = ry[k];
                            vz[idx] = rz[j];
                            vw[idx] = rw[i];
                        }
                    }
                }
            }

            return (
                new ComplexVector<N>(vx, cloning: false),
                new ComplexVector<N>(vy, cloning: false),
                new ComplexVector<N>(vz, cloning: false),
                new ComplexVector<N>(vw, cloning: false)
            );
        }
    }
}
