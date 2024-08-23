using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public static ComplexVector<N> Concat(params object[] blocks) {
            List<Complex<N>> v = [];

            foreach (object obj in blocks) {
                if (obj is ComplexVector<N> cvector) {
                    v.AddRange(cvector.v);
                }
                if (obj is Vector<N> vector) {
                    v.AddRange(vector.Select(v => (Complex<N>)v));
                }
                else if (obj is Complex<N> vmpc) {
                    v.Add(vmpc);
                }
                else if (obj is MultiPrecision<N> vmp) {
                    v.Add(vmp);
                }
                else if (obj is System.Numerics.Complex vdc) {
                    v.Add(vdc);
                }
                else if (obj is double vd) {
                    v.Add(vd);
                }
                else if (obj is int vi) {
                    v.Add(vi);
                }
                else if (obj is long vl) {
                    v.Add(vl);
                }
                else if (obj is float vf) {
                    v.Add(vf);
                }
                else if (obj is string vs) {
                    v.Add(vs);
                }
                else {
                    throw new ArgumentException($"unsupported type '{obj.GetType().Name}'", nameof(blocks));
                }
            }

            return new ComplexVector<N>(v);
        }

        public static ComplexVector<N> Concat(params ComplexVector<N>[] blocks) {
            List<Complex<N>> v = [];

            foreach (ComplexVector<N> vector in blocks) {
                v.AddRange(vector.v);
            }

            return new ComplexVector<N>(v);
        }
    }
}
