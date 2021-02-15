using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SL
{
    public class MathService : IMathService
    {
        public Matrix<double> CalculateMatrix(double[][] Kexp, double[] Zexp)
        {
            Matrix<double> P = Matrix<double>.Build.DenseOfRowArrays(Kexp);
            Vector<double> Z = Vector<double>.Build.DenseOfArray(Zexp);
            Console.WriteLine(P.ToString());

            Vector<double> K = -P.ColumnSums();
            Matrix<double> Padd = P.MapIndexed((i, j, x) => x = i == j ? x = K[i] : x);
            Console.WriteLine(Padd.ToString());

            Matrix<double> Pext = Matrix<double>.Build.DenseOfMatrix(Padd);
            var Kext = K.ToList();
            var Zext = Z.ToList();
            Vector<double> C = Z;
            int index = 0;
            int fullIndex = 0;
            while (Pext.QR().Determinant == 0.0 || index > K.Count - 1)
            {
                if (index > K.Count - 1) throw new Exception("no solution");
                if (Kext[index] == 0.0)
                {
                    Pext = Pext.RemoveColumn(index);
                    Pext = Pext.RemoveRow(index);
                    Kext.RemoveAt(index);
                    Zext.RemoveAt(index);
                    C[fullIndex] = 0.0;
                }
                else
                    index++;
                fullIndex++;
            }
            Console.WriteLine(C.ToString());
            Console.WriteLine(Pext.ToString());

            var qr = Pext.QR();
            Console.WriteLine("QR determinant: {0}", qr.Determinant);
            Console.WriteLine("QR full rank: {0}", qr.IsFullRank);
            // Using Linq to determine the numerical rank with threshold 1e-8.
            Console.WriteLine("QR numerical rank: {0}", qr.R.Diagonal().Count(a => Math.Abs(a) > 1e-8));
            var ZextV = Vector<double>.Build.DenseOfArray(Zext.ToArray());
            Console.WriteLine(ZextV.ToString());

            var Zres = qr.Solve(ZextV);
            Console.WriteLine(Zres.ToString());

            int k = 0;
            var endCount = Zres.Count;
            C.MapInplace((x) =>
            {
                if (k >= endCount) return x;
                var zVal = System.Math.Round(System.Math.Abs(Zres[k]), 3);
                if (x != 0)
                {
                    x = zVal;
                    k++;
                }
                else if (x == 0 && zVal == 0)
                {
                    x = zVal;
                    k++;
                }
                return x;
            });
            Console.WriteLine(C.ToString());

            P.MapIndexedInplace((i, j, x) => x = x * C[j]);
            Console.WriteLine(P.ToString());
            return P;
        }
    }
}
