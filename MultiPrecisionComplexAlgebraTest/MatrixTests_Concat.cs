using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTest {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void ConcatTest() {
            ComplexMatrix<Pow2.N4> matrix3x2 = ComplexMatrix<Pow2.N4>.Fill(3, 2, value: 2);
            ComplexMatrix<Pow2.N4> matrix3x4 = ComplexMatrix<Pow2.N4>.Fill(3, 4, value: 3);
            ComplexMatrix<Pow2.N4> matrix3x6 = ComplexMatrix<Pow2.N4>.Fill(3, 6, value: 4);
            ComplexMatrix<Pow2.N4> matrix5x2 = ComplexMatrix<Pow2.N4>.Fill(5, 2, value: 5);
            ComplexMatrix<Pow2.N4> matrix5x4 = ComplexMatrix<Pow2.N4>.Fill(5, 4, value: 6);
            ComplexMatrix<Pow2.N4> matrix5x6 = ComplexMatrix<Pow2.N4>.Fill(5, 6, value: 7);
            ComplexMatrix<Pow2.N4> matrix7x2 = ComplexMatrix<Pow2.N4>.Fill(7, 2, value: 8);
            ComplexMatrix<Pow2.N4> matrix7x4 = ComplexMatrix<Pow2.N4>.Fill(7, 4, value: 9);
            ComplexMatrix<Pow2.N4> matrix7x6 = ComplexMatrix<Pow2.N4>.Fill(7, 6, value: 10);

            ComplexVector<Pow2.N4> vector3x1 = ComplexVector<Pow2.N4>.Fill(3, value: -1);
            ComplexVector<Pow2.N4> vector5x1 = ComplexVector<Pow2.N4>.Fill(5, value: -2);
            ComplexVector<Pow2.N4> vector7x1 = ComplexVector<Pow2.N4>.Fill(7, value: -3);

            ComplexMatrix<Pow2.N4> matrix1 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix<Pow2.N4> matrix2 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix<Pow2.N4> matrix3 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);

            ComplexMatrix<Pow2.N4> matrix4 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, vector3x1, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix4[.., ..2]);
            Assert.AreEqual(vector3x1, matrix4[.., 2]);
            Assert.AreEqual(matrix3x6, matrix4[.., 3..]);

            ComplexMatrix<Pow2.N4> matrix5 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { vector3x1, matrix3x2, matrix3x6 } });
            Assert.AreEqual(vector3x1, matrix5[.., 0]);
            Assert.AreEqual(matrix3x2, matrix5[.., 1..3]);
            Assert.AreEqual(matrix3x6, matrix5[.., 3..]);

            ComplexMatrix<Pow2.N4> matrix6 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x6, vector3x1 } });
            Assert.AreEqual(matrix3x2, matrix6[.., 0..2]);
            Assert.AreEqual(matrix3x6, matrix6[.., 2..8]);
            Assert.AreEqual(vector3x1, matrix6[.., 8]);

            ComplexMatrix<Pow2.N4> matrix7 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, vector3x1, matrix3x6 }, { matrix5x2, matrix5x4, vector5x1, matrix5x6 }, { matrix7x2, matrix7x4, vector7x1, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix7[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix7[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix7[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix7[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix7[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix7[8.., 2..6]);
            Assert.AreEqual(vector3x1, matrix7[..3, 6]);
            Assert.AreEqual(vector5x1, matrix7[3..8, 6]);
            Assert.AreEqual(vector7x1, matrix7[8.., 6]);
            Assert.AreEqual(matrix3x6, matrix7[..3, 7..]);
            Assert.AreEqual(matrix5x6, matrix7[3..8, 7..]);
            Assert.AreEqual(matrix7x6, matrix7[8.., 7..]);

            ComplexMatrix<Pow2.N4> matrix8 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { vector3x1 }, { vector5x1 }, { vector7x1 } });
            Assert.AreEqual(vector3x1, matrix8[..3, 0]);
            Assert.AreEqual(vector5x1, matrix8[3..8, 0]);
            Assert.AreEqual(vector7x1, matrix8[8.., 0]);

            ComplexMatrix<Pow2.N4> matrix9 = ComplexMatrix<Pow2.N4>.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { (int)(-10), (long)(-20L), MultiPrecision<Pow2.N4>.E, 4.3d, 4.2f, "4.1" },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix9[8, 0]);
            Assert.AreEqual(-20, matrix9[8, 1]);
            Assert.AreEqual(MultiPrecision<Pow2.N4>.E, matrix9[8, 2]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.3d, matrix9[8, 3]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.2f, matrix9[8, 4]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)"4.1", matrix9[8, 5]);

            ComplexMatrix<Pow2.N4> matrix10 = ComplexMatrix<Pow2.N4>.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { (int)(-10), (long)(-20L), MultiPrecision<Pow2.N4>.E, 4.3d, 4.2f, "4.1" },
                    { (int)(-10), (long)(-20L), Complex<Pow2.N4>.ImaginaryOne, 4.3d, 4.2f, "4.1" },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix10[3, 0]);
            Assert.AreEqual(-20, matrix10[3, 1]);
            Assert.AreEqual(MultiPrecision<Pow2.N4>.E, matrix10[3, 2]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.3d, matrix10[3, 3]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.2f, matrix10[3, 4]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)"4.1", matrix10[3, 5]);
            Assert.AreEqual(Complex<Pow2.N4>.ImaginaryOne, matrix10[4, 2]);

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2 }, { matrix3x4 }, { matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 }, { matrix3x4, matrix5x4, matrix7x4 }, { matrix3x6, matrix5x6, matrix7x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2 }, { matrix5x4 }, { matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix5x2, matrix3x4, matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, 'b', matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });
        }

        [TestMethod()]
        public void ConcatMatrixTest() {
            ComplexMatrix<Pow2.N4> matrix3x2 = ComplexMatrix<Pow2.N4>.Fill(3, 2, value: 2);
            ComplexMatrix<Pow2.N4> matrix3x4 = ComplexMatrix<Pow2.N4>.Fill(3, 4, value: 3);
            ComplexMatrix<Pow2.N4> matrix3x6 = ComplexMatrix<Pow2.N4>.Fill(3, 6, value: 4);
            ComplexMatrix<Pow2.N4> matrix5x2 = ComplexMatrix<Pow2.N4>.Fill(5, 2, value: 5);
            ComplexMatrix<Pow2.N4> matrix5x4 = ComplexMatrix<Pow2.N4>.Fill(5, 4, value: 6);
            ComplexMatrix<Pow2.N4> matrix5x6 = ComplexMatrix<Pow2.N4>.Fill(5, 6, value: 7);
            ComplexMatrix<Pow2.N4> matrix7x2 = ComplexMatrix<Pow2.N4>.Fill(7, 2, value: 8);
            ComplexMatrix<Pow2.N4> matrix7x4 = ComplexMatrix<Pow2.N4>.Fill(7, 4, value: 9);
            ComplexMatrix<Pow2.N4> matrix7x6 = ComplexMatrix<Pow2.N4>.Fill(7, 6, value: 10);

            ComplexMatrix<Pow2.N4> matrix1 = ComplexMatrix<Pow2.N4>.Concat(new ComplexMatrix<Pow2.N4>[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix<Pow2.N4> matrix2 = ComplexMatrix<Pow2.N4>.Concat(new ComplexMatrix<Pow2.N4>[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix<Pow2.N4> matrix3 = ComplexMatrix<Pow2.N4>.Concat(new ComplexMatrix<Pow2.N4>[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);
        }

        [TestMethod()]
        public void VConcatTest() {
            ComplexMatrix<Pow2.N4> matrix = ComplexMatrix<Pow2.N4>.VConcat(
                ComplexVector<Pow2.N4>.Fill(3, 1),
                ComplexVector<Pow2.N4>.Fill(3, 2),
                ComplexVector<Pow2.N4>.Fill(3, 3),
                ComplexVector<Pow2.N4>.Fill(3, 4),
                ComplexVector<Pow2.N4>.Fill(3, 5)
            );

            ComplexMatrix<Pow2.N4> matrix_expected = new Complex<Pow2.N4>[5, 3] {
                { 1, 1, 1 },
                { 2, 2, 2 },
                { 3, 3, 3 },
                { 4, 4, 4 },
                { 5, 5, 5 },
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void HConcatTest() {
            ComplexMatrix<Pow2.N4> matrix = ComplexMatrix<Pow2.N4>.HConcat(
                ComplexVector<Pow2.N4>.Fill(3, 1),
                ComplexVector<Pow2.N4>.Fill(3, 2),
                ComplexVector<Pow2.N4>.Fill(3, 3),
                ComplexVector<Pow2.N4>.Fill(3, 4),
                ComplexVector<Pow2.N4>.Fill(3, 5)
            );

            ComplexMatrix<Pow2.N4> matrix_expected = new Complex<Pow2.N4>[3, 5] {
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 }
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void ConcatRealTest() {
            ComplexMatrix<Pow2.N4> matrix3x2 = ComplexMatrix<Pow2.N4>.Fill(3, 2, value: 2);
            ComplexMatrix<Pow2.N4> matrix3x4 = ComplexMatrix<Pow2.N4>.Fill(3, 4, value: 3);
            ComplexMatrix<Pow2.N4> matrix3x6 = ComplexMatrix<Pow2.N4>.Fill(3, 6, value: 4);
            ComplexMatrix<Pow2.N4> matrix5x2 = ComplexMatrix<Pow2.N4>.Fill(5, 2, value: 5);
            ComplexMatrix<Pow2.N4> matrix5x4 = ComplexMatrix<Pow2.N4>.Fill(5, 4, value: 6);
            ComplexMatrix<Pow2.N4> matrix5x6 = ComplexMatrix<Pow2.N4>.Fill(5, 6, value: 7);
            ComplexMatrix<Pow2.N4> matrix7x2 = ComplexMatrix<Pow2.N4>.Fill(7, 2, value: 8);
            ComplexMatrix<Pow2.N4> matrix7x4 = ComplexMatrix<Pow2.N4>.Fill(7, 4, value: 9);
            ComplexMatrix<Pow2.N4> matrix7x6 = ComplexMatrix<Pow2.N4>.Fill(7, 6, value: 10);

            Vector<Pow2.N4> vector3x1 = Vector<Pow2.N4>.Fill(3, value: -1);
            Vector<Pow2.N4> vector5x1 = Vector<Pow2.N4>.Fill(5, value: -2);
            Vector<Pow2.N4> vector7x1 = Vector<Pow2.N4>.Fill(7, value: -3);

            ComplexMatrix<Pow2.N4> matrix1 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix<Pow2.N4> matrix2 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix<Pow2.N4> matrix3 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);

            ComplexMatrix<Pow2.N4> matrix4 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, vector3x1, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix4[.., ..2]);
            Assert.AreEqual(vector3x1, matrix4[.., 2]);
            Assert.AreEqual(matrix3x6, matrix4[.., 3..]);

            ComplexMatrix<Pow2.N4> matrix5 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { vector3x1, matrix3x2, matrix3x6 } });
            Assert.AreEqual(vector3x1, matrix5[.., 0]);
            Assert.AreEqual(matrix3x2, matrix5[.., 1..3]);
            Assert.AreEqual(matrix3x6, matrix5[.., 3..]);

            ComplexMatrix<Pow2.N4> matrix6 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x6, vector3x1 } });
            Assert.AreEqual(matrix3x2, matrix6[.., 0..2]);
            Assert.AreEqual(matrix3x6, matrix6[.., 2..8]);
            Assert.AreEqual(vector3x1, matrix6[.., 8]);

            ComplexMatrix<Pow2.N4> matrix7 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, vector3x1, matrix3x6 }, { matrix5x2, matrix5x4, vector5x1, matrix5x6 }, { matrix7x2, matrix7x4, vector7x1, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix7[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix7[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix7[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix7[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix7[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix7[8.., 2..6]);
            Assert.AreEqual(vector3x1, matrix7[..3, 6]);
            Assert.AreEqual(vector5x1, matrix7[3..8, 6]);
            Assert.AreEqual(vector7x1, matrix7[8.., 6]);
            Assert.AreEqual(matrix3x6, matrix7[..3, 7..]);
            Assert.AreEqual(matrix5x6, matrix7[3..8, 7..]);
            Assert.AreEqual(matrix7x6, matrix7[8.., 7..]);

            ComplexMatrix<Pow2.N4> matrix8 = ComplexMatrix<Pow2.N4>.Concat(new object[,] { { vector3x1 }, { vector5x1 }, { vector7x1 } });
            Assert.AreEqual(vector3x1, matrix8[..3, 0]);
            Assert.AreEqual(vector5x1, matrix8[3..8, 0]);
            Assert.AreEqual(vector7x1, matrix8[8.., 0]);

            ComplexMatrix<Pow2.N4> matrix9 = ComplexMatrix<Pow2.N4>.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { (int)(-10), (long)(-20L), MultiPrecision<Pow2.N4>.E, 4.3d, 4.2f, "4.1" },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix9[8, 0]);
            Assert.AreEqual(-20, matrix9[8, 1]);
            Assert.AreEqual(MultiPrecision<Pow2.N4>.E, matrix9[8, 2]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.3d, matrix9[8, 3]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.2f, matrix9[8, 4]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)"4.1", matrix9[8, 5]);

            ComplexMatrix<Pow2.N4> matrix10 = ComplexMatrix<Pow2.N4>.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { (int)(-10), (long)(-20L), MultiPrecision<Pow2.N4>.E, 4.3d, 4.2f, "4.1" },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix10[3, 0]);
            Assert.AreEqual(-20, matrix10[3, 1]);
            Assert.AreEqual(MultiPrecision<Pow2.N4>.E, matrix10[3, 2]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.3d, matrix10[3, 3]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)4.2f, matrix10[3, 4]);
            Assert.AreEqual((MultiPrecision<Pow2.N4>)"4.1", matrix10[3, 5]);

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2 }, { matrix3x4 }, { matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 }, { matrix3x4, matrix5x4, matrix7x4 }, { matrix3x6, matrix5x6, matrix7x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2 }, { matrix5x4 }, { matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix5x2, matrix3x4, matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix<Pow2.N4>.Concat(new object[,] { { matrix3x2, 'b', matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });
        }

        [TestMethod()]
        public void ConcatMatrixRealTest() {
            Matrix<Pow2.N4> matrix3x2 = Matrix<Pow2.N4>.Fill(3, 2, value: 2);
            Matrix<Pow2.N4> matrix3x4 = Matrix<Pow2.N4>.Fill(3, 4, value: 3);
            Matrix<Pow2.N4> matrix3x6 = Matrix<Pow2.N4>.Fill(3, 6, value: 4);
            Matrix<Pow2.N4> matrix5x2 = Matrix<Pow2.N4>.Fill(5, 2, value: 5);
            Matrix<Pow2.N4> matrix5x4 = Matrix<Pow2.N4>.Fill(5, 4, value: 6);
            Matrix<Pow2.N4> matrix5x6 = Matrix<Pow2.N4>.Fill(5, 6, value: 7);
            Matrix<Pow2.N4> matrix7x2 = Matrix<Pow2.N4>.Fill(7, 2, value: 8);
            Matrix<Pow2.N4> matrix7x4 = Matrix<Pow2.N4>.Fill(7, 4, value: 9);
            Matrix<Pow2.N4> matrix7x6 = Matrix<Pow2.N4>.Fill(7, 6, value: 10);

            ComplexMatrix<Pow2.N4> matrix1 = ComplexMatrix<Pow2.N4>.Concat(new ComplexMatrix<Pow2.N4>[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix<Pow2.N4> matrix2 = ComplexMatrix<Pow2.N4>.Concat(new ComplexMatrix<Pow2.N4>[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix<Pow2.N4> matrix3 = ComplexMatrix<Pow2.N4>.Concat(new ComplexMatrix<Pow2.N4>[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);
        }

        [TestMethod()]
        public void VConcatRealTest() {
            ComplexMatrix<Pow2.N4> matrix = ComplexMatrix<Pow2.N4>.VConcat(
                Vector<Pow2.N4>.Fill(3, 1),
                Vector<Pow2.N4>.Fill(3, 2),
                Vector<Pow2.N4>.Fill(3, 3),
                Vector<Pow2.N4>.Fill(3, 4),
                Vector<Pow2.N4>.Fill(3, 5)
            );

            ComplexMatrix<Pow2.N4> matrix_expected = new Complex<Pow2.N4>[5, 3] {
                { 1, 1, 1 },
                { 2, 2, 2 },
                { 3, 3, 3 },
                { 4, 4, 4 },
                { 5, 5, 5 },
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void HConcatRealTest() {
            ComplexMatrix<Pow2.N4> matrix = ComplexMatrix<Pow2.N4>.HConcat(
                Vector<Pow2.N4>.Fill(3, 1),
                Vector<Pow2.N4>.Fill(3, 2),
                Vector<Pow2.N4>.Fill(3, 3),
                Vector<Pow2.N4>.Fill(3, 4),
                Vector<Pow2.N4>.Fill(3, 5)
            );

            ComplexMatrix<Pow2.N4> matrix_expected = new Complex<Pow2.N4>[3, 5] {
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 }
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }
    }
}