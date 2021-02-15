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

        private string GetMonthName(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return "1 Январь";
                case 2:
                    return "2 Февраль";
                case 3:
                    return "3 Март";
                case 4:
                    return "4 Апрель";
                case 5:
                    return "5 Май";
                case 6:
                    return "6 Июнь";
                case 7:
                    return "7 Июль";
                case 8:
                    return "8 Август";
                case 9:
                    return "9 Сентябрь";
                case 10:
                    return "10 Октябрь";
                case 11:
                    return "11 Ноябрь";
                case 12:
                    return "12 Декабрь";
                default:
                    return string.Empty;
            }
        }

        private int GetQuarter(DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 3)
                return 1;
            else if (date.Month >= 4 && date.Month <= 6)
                return 2;
            else if (date.Month >= 7 && date.Month <= 9)
                return 3;
            else
                return 4;
        }
    }
}
