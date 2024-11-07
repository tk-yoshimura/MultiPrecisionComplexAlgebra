using MultiPrecision;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void EigenValuesN2Test() {
            for (int i = 0; i < 64; i++) {
                ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(2, 2);

                if (m.Any(v => Complex<Pow2.N4>.IsZero(v.val))) {
                    continue;
                }

                if (i < 4) {
                    m[1, 0] = 0;
                }

                (Complex<Pow2.N4>[] vals, ComplexVector<Pow2.N4>[] vecs) = ComplexMatrix<Pow2.N4>.EigenValueVectors(m);

                Console.WriteLine(m);

                for (int j = 0; j < vals.Length; j++) {
                    Complex<Pow2.N4> val = vals[j];
                    ComplexVector<Pow2.N4> vec = vecs[j];

                    ComplexVector<Pow2.N4> a = m * vec;
                    ComplexVector<Pow2.N4> b = val * vec;

                    Console.WriteLine(vec);
                    Console.WriteLine(val);
                    Console.WriteLine(a);
                    Console.WriteLine(b);
                    Console.WriteLine("");

                    Assert.IsTrue((a - b).Norm < 1e-35);
                }

                Console.WriteLine("");
            }
        }

        [TestMethod()]
        public void EigenValuesN3Test() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ "-1-4i", "-2+1i", "-6-2i" },
                 { "7+6i", "2+4i", "-6+6i" },
                 { "6-2i", "4-1i", "7+1i" } }
            );

            (Complex<Pow2.N4>[] vals, ComplexVector<Pow2.N4>[] vecs) = ComplexMatrix<Pow2.N4>.EigenValueVectors(m);

            for (int i = 0; i < vals.Length; i++) {
                Complex<Pow2.N4> val = vals[i];
                ComplexVector<Pow2.N4> vec = vecs[i];

                ComplexVector<Pow2.N4> a = m * vec;
                ComplexVector<Pow2.N4> b = val * vec;

                Console.WriteLine(vec);
                Console.WriteLine(val);
                Console.WriteLine(a);
                Console.WriteLine(b);

                Assert.IsTrue((a - b).Norm < 1e-35);
            }
        }

        [TestMethod()]
        public void EigenValuesN4Test() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ ( 1, + 2), ( 3, - 1), ( 5, + 0), ( 4, + 2) },
                 { ( 4, + 0), ( 6, + 2), ( 8, - 3), ( 3, + 1) },
                 { ( 7, - 1), ( 9, + 1), (11, - 2), (12, - 3) },
                 { (10, + 0), (12, - 1), (14, + 2), ( 7, - 4) } }
            );

            (Complex<Pow2.N4>[] vals, ComplexVector<Pow2.N4>[] vecs) = ComplexMatrix<Pow2.N4>.EigenValueVectors(m);

            for (int i = 0; i < vals.Length; i++) {
                Complex<Pow2.N4> val = vals[i];
                ComplexVector<Pow2.N4> vec = vecs[i];

                ComplexVector<Pow2.N4> a = m * vec;
                ComplexVector<Pow2.N4> b = val * vec;

                Console.WriteLine(vec);
                Console.WriteLine(val);
                Console.WriteLine(a);
                Console.WriteLine(b);

                Assert.IsTrue((a - b).Norm < 1e-35);
            }
        }

        [TestMethod()]
        public void EigenValuesTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 32; i++) {
                    ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(n, n);

                    if (m.Any(v => Complex<Pow2.N4>.IsZero(v.val))) {
                        continue;
                    }

                    Complex<Pow2.N4>[] vals_withoutvec = ComplexMatrix<Pow2.N4>.EigenValues(m);
                    (Complex<Pow2.N4>[] vals, ComplexVector<Pow2.N4>[] vecs) = ComplexMatrix<Pow2.N4>.EigenValueVectors(m);

                    CollectionAssert.AreEqual(vals_withoutvec, vals);

                    Console.WriteLine(m);

                    for (int j = 0; j < vals.Length; j++) {
                        Complex<Pow2.N4> val = vals[j];
                        ComplexVector<Pow2.N4> vec = vecs[j];

                        ComplexVector<Pow2.N4> a = m * vec;
                        ComplexVector<Pow2.N4> b = val * vec;

                        Console.WriteLine(vec);
                        Console.WriteLine(val);
                        Console.WriteLine(a);
                        Console.WriteLine(b);
                        Console.WriteLine("");

                        Assert.IsTrue((a - b).Norm < 1e-34);
                    }

                    Console.WriteLine("");
                }
            }
        }

        [TestMethod()]
        public void EigenVectorTest() {
            foreach (ComplexMatrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.N2Matrixs) {
                Console.WriteLine($"test: {matrix}");

                ComplexMatrix<Pow2.N4> matrix_scaled = ComplexMatrix<Pow2.N4>.ScaleB(matrix, -matrix.MaxExponent);

                (Complex<Pow2.N4>[] eigen_values, ComplexVector<Pow2.N4>[] eigen_vectors) = ComplexMatrix<Pow2.N4>.EigenValueVectors(matrix_scaled);
                ComplexVector<Pow2.N4> eigen_values_expected = ComplexMatrix<Pow2.N4>.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    Complex<Pow2.N4> eigen_value = eigen_values[i];
                    ComplexVector<Pow2.N4> eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-32);
                }
            }

            foreach (ComplexMatrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                ComplexMatrix<Pow2.N4> matrix_scaled = ComplexMatrix<Pow2.N4>.ScaleB(matrix, -matrix.MaxExponent);

                (Complex<Pow2.N4>[] eigen_values, ComplexVector<Pow2.N4>[] eigen_vectors) = ComplexMatrix<Pow2.N4>.EigenValueVectors(matrix_scaled);
                ComplexVector<Pow2.N4> eigen_values_expected = ComplexMatrix<Pow2.N4>.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    Complex<Pow2.N4> eigen_value = eigen_values[i];
                    ComplexVector<Pow2.N4> eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-32);
                }
            }

            foreach (ComplexMatrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.SingularMatrixs[..32]) {
                Console.WriteLine($"test: {matrix}");

                ComplexMatrix<Pow2.N4> matrix_scaled = ComplexMatrix<Pow2.N4>.ScaleB(matrix, -matrix.MaxExponent);

                (Complex<Pow2.N4>[] eigen_values, ComplexVector<Pow2.N4>[] eigen_vectors) = ComplexMatrix<Pow2.N4>.EigenValueVectors(matrix_scaled);
                ComplexVector<Pow2.N4> eigen_values_expected = ComplexMatrix<Pow2.N4>.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    Complex<Pow2.N4> eigen_value = eigen_values[i];
                    ComplexVector<Pow2.N4> eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-32);
                }
            }
        }
    }
}