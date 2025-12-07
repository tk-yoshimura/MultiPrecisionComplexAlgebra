# MultiPrecisionComplexAlgebra
 MultiPrecision Complex Algebra Implements 

## Requirement
.NET 10.0  
AVX2 suppoted CPU. (Intel:Haswell(2013)-, AMD:Excavator(2015)-)  
[MultiPrecision](https://github.com/tk-yoshimura/MultiPrecision)  
[MultiPrecisionComplex](https://github.com/tk-yoshimura/MultiPrecisionComplex)  
[MultiPrecisionAlgebra](https://github.com/tk-yoshimura/MultiPrecisionAlgebra)  

## Install

[Download DLL](https://github.com/tk-yoshimura/MultiPrecisionComplexAlgebra/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.multiprecision.complexalgebra/)

## Usage

```csharp
// solve for v: Av=x
ComplexMatrix<Pow2.N4> a = new Complex<Pow2.N4>[,] { { (1, 1), (1, 2) }, { (1, 3), (4, -1) } };
ComplexVector<Pow2.N4> x = ((4, 2), (-1, 3));

ComplexVector<Pow2.N4> v = ComplexMatrix<Pow2.N4>.Solve(a, x);
```

## Licence
[MIT](https://github.com/tk-yoshimura/MultiPrecisionComplexAlgebra/blob/master/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
