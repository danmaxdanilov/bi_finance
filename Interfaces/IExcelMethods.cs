using SL.Models;
using System.Collections.Generic;
using System.IO;

namespace SL
{
    public interface IExcelMethods
    {
        List<Employee> GetEmployeesFromFile(Stream file);
        List<LawyerDistribution> GetLawyerDistributionFromFile(Stream file);
        List<Payment> GetPaymentsFromFile(Stream file);
        List<ProductionCost> GetProductionCostFromFile(Stream file);
        List<SalaryCost> GetSalaryCostFromFile(Stream file);
        List<AdvanceCost> GetAdvanceCostFromFile(Stream file);
        List<RevenueModel> GetRevenuesFromFile(Stream file);
        List<IncomeModel> GetIncomesFromFile(Stream file);
    }
}