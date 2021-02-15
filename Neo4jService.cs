using BI_FINANCE_APP.Model;
using DAL;
using DAL.BIFinance;
using SL.Models;
using System.Collections.Generic;
using static SL.Models.EnumTypes;

namespace SL
{
    public class Neo4jService : INeo4jService
    {
        private readonly Neo4jConnector _neo4jConnector;
        public Neo4jService(string uri, string user, string password)
        {
            _neo4jConnector = new Neo4jConnector(uri, user, password);
        }

        public Dictionary<int, double[]> GetMatrixOfInitialCoefficients() => _neo4jConnector.GetMatrixOfInitialCoefficients();
        public double[] GetVectorOfDirectCost() => _neo4jConnector.GetVectorOfDirectCost();
        public void SetIdentityToCostCenter(int id, int index) => _neo4jConnector.SetIdentityToCostCenter(id, index);
        public void ResetIdentityToCostCenter() => _neo4jConnector.ResetIdentityToCostCenter();
        public void ResetAllIndirectCosts() => _neo4jConnector.ResetAllIndirectCosts();
        public List<LinkArc> GetExistedConnections() => _neo4jConnector.GetExistedConnections();
        public void UpdateIndirectCost(int costCenterSourceId, int costCenterSestinationId, decimal indirectCost) =>
            _neo4jConnector.UpdateIndirectCost(costCenterSourceId, costCenterSestinationId, indirectCost);

        public void CreateEmployee(Employee employee) =>
            _neo4jConnector.CreateEmployee(employee.Name, employee.Position, employee.Office,
                employee.IndirectDepartment,
                employee.DirectDepartment,
                employee.Comments);
        public void CreateBackOffice() => _neo4jConnector.CreateBackOffice();
        public void CreateLinkFromOfficeToLawyer() => _neo4jConnector.CreateLinkFromOfficeToLawyer();
        public void CreateOffice() => _neo4jConnector.CreateOffice();

        public void CreateProductionCost(ProductionCost productionCost) =>
            _neo4jConnector.CreateProductionCost(productionCost.CostCenter, productionCost.ProfitCenter,
                productionCost.CostType, productionCost.Reciever, productionCost.ExpensiveItem,
                productionCost.Amount);
        public void LinkProductionCostToProfitCenter() => _neo4jConnector.LinkProductionCostToProfitCenter();
        public void LinkProductionCostToOffice() => _neo4jConnector.LinkProductionCostToOffice();
        public void LinkProductionCostToQuorum() => _neo4jConnector.LinkProductionCostToQuorum();

        public void CreateSalaryCosts(SalaryCost salaryCost) => 
            _neo4jConnector.CreateSalaryCosts(salaryCost.Employee, salaryCost.SalaryTotal);
        public void MergeSalaryCosts() => _neo4jConnector.MergeSalaryCosts();
        public void SetSalaryCostToAllEmployee() => _neo4jConnector.SetSalaryCostToAllEmployee();
        public void LinkSalaryCostToQuorum() => _neo4jConnector.LinkSalaryCostToQuorum();

        public void CreateAdvanceCost(AdvanceCost advanceCost) =>
            _neo4jConnector.CreateAdvanceCost(advanceCost.CostCenter, advanceCost.ProfitCenter,
        advanceCost.CostType, advanceCost.Employee, advanceCost.Amount);
        public void MergeAdvanceCosts() => _neo4jConnector.MergeAdvanceCosts();
        public void LinkAdvanceCostToProfitCenter() => _neo4jConnector.LinkAdvanceCostToProfitCenter();
        public void LinkAdvanceCostToLawyer() => _neo4jConnector.LinkAdvanceCostToLawyer();
        public void LinkAdvanceCostToOffice() => _neo4jConnector.LinkAdvanceCostToOffice();
        public void LinkAdvanceCostToQuorum() => _neo4jConnector.LinkAdvanceCostToQuorum();

        public void CreatePayments(Payment payment) =>
            _neo4jConnector.CreatePayments(payment.ReceiverId, payment.RecieverName, payment.PaymentTypeId,
                payment.PaymentTypeName, payment.Amount);
        public void MergePayments() => _neo4jConnector.MergePayments();
        public void SetDirectoryCostToAll() => _neo4jConnector.SetDirectoryCostToAll();
        public void LinkPaymentAndOffice() => _neo4jConnector.LinkPaymentAndOffice();
        public void SetEmptyPaymentForOfficeAndDepartment() => _neo4jConnector.SetEmptyPaymentForOfficeAndDepartment();

        public void DeleteChildLinksFromDecreeEmployee() => _neo4jConnector.DeleteChildLinksFromDecreeEmployee();

        public void FillRatioBetweenPaymentAndOffice() => _neo4jConnector.FillRatioBetweenPaymentAndOffice();
        public void FillRatioBetweenBackOfficeAndDepartment() => _neo4jConnector.FillRatioBetweenBackOfficeAndDepartment();
        public void FillRatioBetweenOfficeAndLawyer() => _neo4jConnector.FillRatioBetweenOfficeAndLawyer();
        public void FillRatioBetweenDepartmentAndLawyer() => _neo4jConnector.FillRatioBetweenDepartmentAndLawyer();

        public void CreateProfitCentersFromProjects(int projectId, string projectName) =>
            _neo4jConnector.CreateProfitCentersFromProjects(projectId, projectName);
        public void CreateLinkArcProject(string name, int projectId, double ratio) =>
            _neo4jConnector.CreateLinkArcProject(name, projectId, ratio);
        public void CreateLinkArcProjectMka() => _neo4jConnector.CreateLinkArcProjectMka();
        public void CreateLinkArcProjectOther() => _neo4jConnector.CreateLinkArcProjectOther();

        public List<CostAccounting> GetTrialBalanceDirectionPayments() =>
            _neo4jConnector.GetTrialBalanceDirectionPayments();
        public List<CostAccounting> GetTrialBalanceIndirectionPayments() =>
            _neo4jConnector.GetTrialBalanceIndirectionPayments();
        public List<CostAccountingReport> GetTrialBalanceReport() =>
            _neo4jConnector.GetTrialBalanceReport();

        public void CleanUp() => _neo4jConnector.CleanUp();
        public int CostCenterCount() => _neo4jConnector.CostCenterCount();
        public int CostCenterCount(CostCenterType type) => _neo4jConnector.CostCenterCount(type.GetDisplayName());
    }
}
