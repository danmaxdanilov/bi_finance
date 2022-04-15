using DAL.BIFinance;
using System;
using System.Linq;

namespace SL
{
    public class ExportService : IExportService
    {
        private readonly BIDbContext _bIDbContext;
        private readonly INeo4jService _neo4JService;
        public ExportService(BIDbContext bIDbContext, INeo4jService neo4JService)
        {
            _bIDbContext = bIDbContext;
            _neo4JService = neo4JService;
        }

        public void FillExportTable(DateTime stage, bool force)
        {
            var costAccounting = _bIDbContext.CostAccounting.Where(x => x.Stage.Equals(stage));
            var costAccountingReport = _bIDbContext.CostAccountingReport.Where(x => x.Stage.Equals(stage));
            if (costAccounting.Count() > 0 || costAccountingReport.Count() > 0)
            {
                if (force)
                {
                    _bIDbContext.CostAccounting.RemoveRange(costAccounting);
                    _bIDbContext.CostAccountingReport.RemoveRange(costAccountingReport);
                    _bIDbContext.SaveChanges();
                }
                else
                    throw new Exception("Отчет за данный период уже сформирован, если вы хотите переписать отчет укажите переписать");
            }

            var direction = _neo4JService.GetTrialBalanceDirectionPayments();
            direction.ForEach(r => r.Stage = stage);
            _bIDbContext.CostAccounting.AddRange(direction);
            _bIDbContext.SaveChanges();
            var indirection = _neo4JService.GetTrialBalanceIndirectionPayments();
            indirection.ForEach(r => r.Stage = stage);
            _bIDbContext.CostAccounting.AddRange(indirection);
            _bIDbContext.SaveChanges();

            var report = _neo4JService.GetTrialBalanceReport();
            var quarter = $"Q-{GetQuarter(stage)}";
            var month = $"{GetMonthName(stage)}-{stage.Year}";
            report.ForEach(r => 
            {
                r.Stage = stage;
                r.Year = stage.Year;
                r.Quarter = quarter;
                r.Month = month;
            });
            _bIDbContext.CostAccountingReport.AddRange(report);
            _bIDbContext.SaveChanges();
        }
    }
}
