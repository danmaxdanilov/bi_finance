using System.IO;

namespace SL
{
    public interface IPaymentService
    {
        void FillPaymentsFromFile(Stream file);
        void FillProductionCostFromFile(Stream file);
        void FillSalaryCostFromFile(Stream file);
        void FillAdvanceCostFromFile(Stream file);
    }
}