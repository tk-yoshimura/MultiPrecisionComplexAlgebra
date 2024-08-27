using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public static ComplexMatrix<N> Grid((int min, int max) real, (int min, int max) imag) {
            if (unchecked(real.max - real.min) < 0 || real.max - real.min >= int.MaxValue) {
                throw new ArgumentException("invalid range", nameof(real));
            }

            if (unchecked(imag.max - imag.min) < 0 || imag.max - imag.min >= int.MaxValue) {
                throw new ArgumentException("invalid range", nameof(imag));
            }

            int rows = real.max - real.min + 1;
            int cols = imag.max - imag.min + 1;

            Complex<N>[,] e = new Complex<N>[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    e[i, j] = (real.min + i, imag.min + j);
                }
            }

            return new ComplexMatrix<N>(e, cloning: false);
        }
    }
}
