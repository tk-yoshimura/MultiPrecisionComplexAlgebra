using MultiPrecision;
using MultiPrecisionComplex;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public Complex<N> this[int index] {
            get => v[index];
            set => v[index] = value;
        }

        public Complex<N> this[Index index] {
            get => v[index.GetOffset(Dim)];
            set => v[index.GetOffset(Dim)] = value;
        }

        public ComplexVector<N> this[Range range] {
            get {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                Complex<N>[] ret = new Complex<N>[counts];
                for (int i = 0; i < counts; i++) {
                    ret[i] = v[i + index];
                }

                return new(ret);
            }

            set {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                if (value.Dim != counts) {
                    throw new ArgumentOutOfRangeException(nameof(range));
                }

                for (int i = 0; i < counts; i++) {
                    v[i + index] = value.v[i];
                }
            }
        }

        public ComplexVector<N> this[int[] indexes] {
            get {
                Complex<N>[] ret = new Complex<N>[indexes.Length];
                for (int i = 0; i < indexes.Length; i++) {
                    ret[i] = v[indexes[i]];
                }

                return new(ret);
            }

            set {
                if (value.Dim != indexes.Length) {
                    throw new ArgumentException("invalid size", nameof(indexes));
                }

                for (int i = 0; i < indexes.Length; i++) {
                    v[indexes[i]] = value.v[i];
                }
            }
        }

        public ComplexVector<N> this[IEnumerable<int> indexes] {
            get => this[indexes.ToArray()];
            set => this[indexes.ToArray()] = value;
        }
    }
}
