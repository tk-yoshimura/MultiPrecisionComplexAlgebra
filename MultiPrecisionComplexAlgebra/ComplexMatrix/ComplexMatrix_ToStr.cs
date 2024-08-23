﻿using MultiPrecision;
using System.Text;

namespace MultiPrecisionComplexAlgebra {
    public partial class ComplexMatrix<N> where N : struct, IConstant {
        public override string ToString() {
            if (!IsValid(this)) {
                return "invalid";
            }

            StringBuilder str = new($"[ [ {e[0, 0]}");
            for (int j = 1; j < Columns; j++) {
                str.Append($", {e[0, j]}");
            }
            str.Append(" ]");

            for (int i = 1; i < Rows; i++) {
                str.Append($", [ {e[i, 0]}");
                for (int j = 1; j < Columns; j++) {
                    str.Append($", {e[i, j]}");
                }
                str.Append(" ]");
            }

            str.Append(" ]");

            return str.ToString();
        }

        public string ToString(string format) {
            if (!IsValid(this)) {
                return "invalid";
            }

            StringBuilder str = new($"[ [ {e[0, 0].ToString(format)}");
            for (int j = 1; j < Columns; j++) {
                str.Append($", {e[0, j].ToString(format)}");
            }
            str.Append(" ]");

            for (int i = 1; i < Rows; i++) {
                str.Append($", [ {e[i, 0].ToString(format)}");
                for (int j = 1; j < Columns; j++) {
                    str.Append($", {e[i, j].ToString(format)}");
                }
                str.Append(" ]");
            }

            str.Append(" ]");

            return str.ToString();
        }

        public string ToString(string format, IFormatProvider provider) {
            if (!IsValid(this)) {
                return "invalid";
            }

            StringBuilder str = new($"[ [ {e[0, 0].ToString(format, provider)}");
            for (int j = 1; j < Columns; j++) {
                str.Append($", {e[0, j].ToString(format, provider)}");
            }
            str.Append(" ]");

            for (int i = 1; i < Rows; i++) {
                str.Append($", [ {e[i, 0].ToString(format, provider)}");
                for (int j = 1; j < Columns; j++) {
                    str.Append($", {e[i, j].ToString(format, provider)}");
                }
                str.Append(" ]");
            }

            str.Append(" ]");

            return str.ToString();
        }
    }
}
