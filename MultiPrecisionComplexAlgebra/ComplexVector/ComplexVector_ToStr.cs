using MultiPrecision;
using System.Text;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexVector<N> where N : struct, IConstant {
        public override string ToString() {
            if (!IsValid(this)) {
                return "invalid";
            }

            StringBuilder str = new($"[ {v[0]}");

            for (int i = 1; i < Dim; i++) {
                str.Append($", {v[i]}");
            }

            str.Append(" ]");

            return str.ToString();
        }

        public string ToString(string format) {
            if (!IsValid(this)) {
                return "invalid";
            }

            StringBuilder str = new($"[ {v[0].ToString(format)}");

            for (int i = 1; i < Dim; i++) {
                str.Append($", {v[i].ToString(format)}");
            }

            str.Append(" ]");

            return str.ToString();
        }

        public string ToString(string format, IFormatProvider provider) {
            if (!IsValid(this)) {
                return "invalid";
            }

            StringBuilder str = new($"[ {v[0].ToString(format, provider)}");

            for (int i = 1; i < Dim; i++) {
                str.Append($", {v[i].ToString(format, provider)}");
            }

            str.Append(" ]");

            return str.ToString();
        }
    }
}
