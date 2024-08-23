using MultiPrecision;
using MultiPrecisionComplex;
using System.Diagnostics;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex<N> Trace {
            get {
                Complex<N> sum = Complex<N>.Zero;
                foreach (var diagonal in Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
