using System.IO;

namespace SL
{
    public interface IDistributionService
    {
        void FillLawyerDistribution(Stream file);
    }
}