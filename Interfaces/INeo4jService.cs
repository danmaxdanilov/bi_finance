using System.Collections.Generic;
using BI_FINANCE_APP.Model;
using DAL.BIFinance;
using SL.Models;

namespace SL
{
    public interface INeo4jService
    {
        void CleanUp();
        int CostCenterCount();
        int CostCenterCount(EnumTypes.CostCenterType type);
        void CreateAdvanceCost(AdvanceCost advanceCost);
        void CreateBackOffice();
        void CreateEmployee(Employee employee);
        void CreateLinkArcProject(string name, int projectId, double ratio);
        void CreateLinkArcProjectMka();
        void CreateLinkArcProjectOther();
        void CreateLinkFromOfficeToLawyer();
        void CreateOffice();
        void CreatePayments(Payment payment);
        void CreateProductionCost(ProductionCost productionCost);
        void CreateProfitCentersFromProjects(int projectId, string projectName);
        void CreateSalaryCosts(SalaryCost salaryCost);
        void DeleteChildLinksFromDecreeEmployee();
        void FillRatioBetweenBackOfficeAndDepartment();
        void FillRatioBetweenDepartmentAndLawyer();
        void FillRatioBetweenOfficeAndLawyer();
        void FillRatioBetweenPaymentAndOffice();
        List<LinkArc> GetExistedConnections();
        Dictionary<int, double[]> GetMatrixOfInitialCoefficients();
        List<CostAccounting> GetTrialBalanceDirectionPayments();
        List<CostAccounting> GetTrialBalanceIndirectionPayments();
        List<CostAccountingReport> GetTrialBalanceReport();
        double[] GetVectorOfDirectCost();
        void LinkAdvanceCostToLawyer();
        void LinkAdvanceCostToOffice();
        void LinkAdvanceCostToProfitCenter();
        void LinkAdvanceCostToQuorum();
        void LinkPaymentAndOffice();
        void LinkProductionCostToOffice();
        void LinkProductionCostToProfitCenter();
        void LinkProductionCostToQuorum();
        void LinkSalaryCostToQuorum();
        void MergeAdvanceCosts();
        void MergePayments();
        void MergeSalaryCosts();
        void ResetAllIndirectCosts();
        void ResetIdentityToCostCenter();
        void SetDirectoryCostToAll();
        void SetEmptyPaymentForOfficeAndDepartment();
        void SetIdentityToCostCenter(int id, int index);
        void SetSalaryCostToAllEmployee();
        void UpdateIndirectCost(int costCenterSourceId, int costCenterSestinationId, decimal indirectCost);
    }
}