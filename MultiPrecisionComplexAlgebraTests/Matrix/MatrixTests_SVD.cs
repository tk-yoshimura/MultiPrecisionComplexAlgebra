using MultiPrecision;
using MultiPrecisionComplex;
using MultiPrecisionComplexAlgebra;

namespace MultiPrecisionComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void SVD4x4Test() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ ( 1, +2), ( 3, -1), ( 5, +0), ( 4, +2) },
                 { ( 4, +0), ( 6, +2), ( 8, -3), ( 3, +1) },
                 { ( 7, -1), ( 9, +1), (11, -2), (12, -3) },
                 { ( 6, -2), ( 4, -1), ( 7,  1), ( 0,  3) } }
            );

            (ComplexMatrix<Pow2.N4> u, ComplexVector<Pow2.N4> s, ComplexMatrix<Pow2.N4> v) = ComplexMatrix<Pow2.N4>.SVD(m);

            ComplexMatrix<Pow2.N4> uu = u * u.H;
            ComplexMatrix<Pow2.N4> vv = v * v.H;

            ComplexMatrix<Pow2.N4> a = u * ComplexMatrix<Pow2.N4>.FromDiagonals(s) * v.H;

            Assert.IsLessThan(1e-30, u.Det.Norm - 1);
            Assert.IsLessThan(1e-30, (uu - ComplexMatrix<Pow2.N4>.Identity(4)).Det.Norm);

            Assert.IsLessThan(1e-30, v.Det.Norm - 1);
            Assert.IsLessThan(1e-30, (vv - ComplexMatrix<Pow2.N4>.Identity(4)).Det.Norm);

            Assert.IsLessThan(1e-30, MultiPrecision<Pow2.N4>.Abs((m - a).Norm));
        }

        [TestMethod()]
        public void SVD5x4Test() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ ( 1, +2), ( 3, -1), ( 5, +0), ( 4, +2) },
                 { ( 4, +0), ( 6, +2), ( 8, -3), ( 3, +1) },
                 { ( 7, -1), ( 9, +1), (11, -2), (12, -3) },
                 { (10, +0), (12, -1), (14, +2), ( 7, -4) },
                 { ( 6, -2), ( 4, -1), ( 7,  1), ( 0,  3) } }
            );

            (ComplexMatrix<Pow2.N4> u, ComplexVector<Pow2.N4> s, ComplexMatrix<Pow2.N4> v) = ComplexMatrix<Pow2.N4>.SVD(m);

            ComplexMatrix<Pow2.N4> uu = u * u.H;
            ComplexMatrix<Pow2.N4> vv = v * v.H;

            ComplexMatrix<Pow2.N4> sm = ComplexMatrix<Pow2.N4>.VConcat(ComplexMatrix<Pow2.N4>.FromDiagonals(s), ComplexMatrix<Pow2.N4>.Zero(1, 4));
            ComplexMatrix<Pow2.N4> a = u * sm * v.H;

            Assert.IsLessThan(1e-30, u.Det.Norm - 1);
            Assert.IsLessThan(1e-30, (uu - ComplexMatrix<Pow2.N4>.Identity(5)).Det.Norm);

            Assert.IsLessThan(1e-30, v.Det.Norm - 1);
            Assert.IsLessThan(1e-30, (vv - ComplexMatrix<Pow2.N4>.Identity(4)).Det.Norm);

            Assert.IsLessThan(1e-30, MultiPrecision<Pow2.N4>.Abs((m - a).Norm));
        }

        [TestMethod()]
        public void SVD4x5Test() {
            ComplexMatrix<Pow2.N4> m = new(new Complex<Pow2.N4>[,]
                {{ ( 1, +2), ( 3, -1), ( 5, +0), ( 4, +2), (6, -2) },
                 { ( 4, +0), ( 6, +2), ( 8, -3), ( 3, +1), (4, -1) },
                 { ( 7, -1), ( 9, +1), (11, -2), (12, -3), (7,  1) },
                 { (10, +0), (12, -1), (14, +2), ( 7, -4), (0,  3) } }
            );

            (ComplexMatrix<Pow2.N4> u, ComplexVector<Pow2.N4> s, ComplexMatrix<Pow2.N4> v) = ComplexMatrix<Pow2.N4>.SVD(m);

            ComplexMatrix<Pow2.N4> uu = u * u.H;
            ComplexMatrix<Pow2.N4> vv = v * v.H;

            ComplexMatrix<Pow2.N4> sm = ComplexMatrix<Pow2.N4>.HConcat(ComplexMatrix<Pow2.N4>.FromDiagonals(s), ComplexMatrix<Pow2.N4>.Zero(4, 1));
            ComplexMatrix<Pow2.N4> a = u * sm * v.H;

            Assert.IsLessThan(1e-30, u.Det.Norm - 1);
            Assert.IsLessThan(1e-30, (uu - ComplexMatrix<Pow2.N4>.Identity(4)).Det.Norm);

            Assert.IsLessThan(1e-30, v.Det.Norm - 1);
            Assert.IsLessThan(1e-30, (vv - ComplexMatrix<Pow2.N4>.Identity(5)).Det.Norm);

            Assert.IsLessThan(1e-30, MultiPrecision<Pow2.N4>.Abs((m - a).Norm));
        }

        [TestMethod()]
        public void SVDTest() {
            for (int rows = 1; rows <= 8; rows++) {
                for (int cols = 1; cols <= 8; cols++) {
                    Console.WriteLine($"{rows}x{cols}");

                    for (int i = 0; i < 16; i++) {
                        ComplexMatrix<Pow2.N4> m = TestCase<Pow2.N4>.RandomMatrix(rows, cols);

                        Console.WriteLine(m);

                        (ComplexMatrix<Pow2.N4> u, ComplexVector<Pow2.N4> s, ComplexMatrix<Pow2.N4> v) = ComplexMatrix<Pow2.N4>.SVD(m);

                        ComplexMatrix<Pow2.N4> uu = u * u.H;
                        ComplexMatrix<Pow2.N4> vv = v * v.H;

                        ComplexMatrix<Pow2.N4> sm;

                        if (rows == cols) {
                            sm = ComplexMatrix<Pow2.N4>.FromDiagonals(s);
                        }
                        else if (rows < cols) {
                            sm = ComplexMatrix<Pow2.N4>.HConcat(ComplexMatrix<Pow2.N4>.FromDiagonals(s), ComplexMatrix<Pow2.N4>.Zero(rows, cols - rows));
                        }
                        else {
                            sm = ComplexMatrix<Pow2.N4>.VConcat(ComplexMatrix<Pow2.N4>.FromDiagonals(s), ComplexMatrix<Pow2.N4>.Zero(rows - cols, cols));
                        }

                        ComplexMatrix<Pow2.N4> a = u * sm * v.H;

                        Console.WriteLine(uu);
                        Console.WriteLine(vv);
                        Console.WriteLine(a);

                        Assert.IsLessThan(1e-30, u.Det.Norm - 1);
                        Assert.IsLessThan(1e-30, (uu - ComplexMatrix<Pow2.N4>.Identity(rows)).Det.Norm);

                        Assert.IsLessThan(1e-30, v.Det.Norm - 1);
                        Assert.IsLessThan(1e-30, (vv - ComplexMatrix<Pow2.N4>.Identity(cols)).Det.Norm);

                        Assert.IsLessThan(1e-30, MultiPrecision<Pow2.N4>.Abs((m - a).Norm));

                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}