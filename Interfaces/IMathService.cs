using MathNet.Numerics.LinearAlgebra;

namespace SL
{
    public interface IMathService
    {
        void Test();
        Matrix<double> CalculateMatrix(double[][] Kexp, double[] Zexp);
    }
}