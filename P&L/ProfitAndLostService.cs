using DAL.BIFinance;
using Microsoft.EntityFrameworkCore;
using SL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SL
{
    public interface IProfitAndLostService
    {
        void ImportRevenues(Stream stream);
        void ImportIncomes(Stream stream);
    }

    public class ProfitAndLostService : IProfitAndLostService
    {
        private readonly IExcelMethods _excelMethods;
        private readonly BIDbContext _context;
        private readonly List<Stage> _stagesInDb;

        public ProfitAndLostService(IExcelMethods excelMethods, BIDbContext context)
        {
            _excelMethods = excelMethods;
            _context = context;
            _stagesInDb = _context.CostAccountingReport
                .Select(x =>
                    new Stage
                    {
                        StageDate = x.Stage,
                        StageMonthName = x.Month,
                        StageMonth = x.Stage.Month,
                        StageYear = x.Year,
                        StageQuarter = x.Quarter
                    })
                .Distinct()
                .ToList();
        }

        public void ImportRevenues(Stream stream)
        {
            var revenues = _excelMethods.GetRevenuesFromFile(stream);
            revenues.ForEach(revenue =>
            {
                var invoiceId = FindOrCreateInvoice(revenue);
                AddInvoiceOperation(invoiceId, "Revenue", revenue);
            });
        }

        public void ImportIncomes(Stream stream)
        {
            var incomes = _excelMethods.GetIncomesFromFile(stream);
            incomes.ForEach(income =>
            {
                var invoiceId = FindOrCreateInvoice(income);
                AddInvoiceOperation(invoiceId, "Income", income);
            });
        }

        private int FindOrCreateInvoice(RevenueModel revenue)
        {
            Invoice invoice;
            if (!revenue.InvoiceDate.HasValue)
                throw new Exception("Неверно задана дата операции");
            var stage = GetCorrectStage(revenue.StageName);
            invoice = _context.Invoice
                .FirstOrDefault(x =>
                    x.ProfitCenter == revenue.ProfitCenter &&
                    x.Year == stage.StageYear &&
                    x.Month == stage.StageMonth &&
                    x.Line == revenue.Line);
            if (invoice == null)
            {
                invoice = new Invoice();
                invoice.ProfitCenter = revenue.ProfitCenter;
                invoice.Number = revenue.InvoiceNumber;
                invoice.Name = revenue.InvoiceName;
                invoice.Year = stage.StageYear;
                invoice.Month = stage.StageMonth;
                invoice.Line = revenue.Line;
                _context.Invoice.Add(invoice);
                _context.SaveChanges();
            }
            return invoice.Id;
        }
        private int FindOrCreateInvoice(IncomeModel income)
        {
            Invoice invoice;
            if (!income.InvoiceDate.HasValue)
                throw new Exception("Неверно задана дата операции");
            var stage = GetCorrectStage(income.StageName);
            invoice = _context.Invoice
                .FirstOrDefault(x =>
                    x.ProfitCenter == income.ProfitCenter &&
                    x.Year == stage.StageYear &&
                    x.Month == stage.StageMonth &&
                    x.Line == income.Line)
                    
                    Create(invoice);
                    
            return invoice.Id;
        }
        
        private void Create(Invoice invoice)
        {        
                invoice = _context.Invoice
                    .FirstOrDefault(x => x.Number == income.InvoiceNumber + 100000 && x.Year == stage.StageYear);
                if (invoice == null)
                {
                    invoice = new Invoice();
                    invoice.ProfitCenter = income.ProfitCenter;
                    invoice.Number = income.InvoiceNumber + 100000;
                    invoice.Name = "Счет по " + income.InvoiceName + " (не соотнесен)";
                    invoice.Year = stage.StageYear;
                    invoice.Month = stage.StageMonth;
                    invoice.Line = income.Line;
                    _context.Invoice.Add(invoice);
                    _context.SaveChanges();
                }
        }

        private void AddInvoiceOperation(int invoiceId, string type, RevenueModel revenue)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var invoiceOperation = new InvoiceOperation();
                    invoiceOperation.InvoiceId = invoiceId;
                    invoiceOperation.Type = type;
                    invoiceOperation.Amount = revenue.Amount;

                    var stage = GetCorrectStage(revenue.StageName);
                    invoiceOperation.Stage = stage.StageDate;
                    invoiceOperation.Year = stage.StageYear;
                    invoiceOperation.Month = stage.StageMonthName;

                    _context.Add(invoiceOperation);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException as SqlException;
                    if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                    {
                        Console.WriteLine($"Операция [{revenue.InvoiceName}] на сумму [{revenue.Amount}] Р. уже загружена в систему. \r\n");
                    }
                    else
                    {
                        Console.WriteLine($"Операция [{revenue.InvoiceName}] на сумму [{revenue.Amount}] Р. вызвала ошибку. \r\n");
                        Console.WriteLine(ex.Message);
                    }
                    transaction.Rollback();
                    RollBack();
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    RollBack();
                }
            }
        }

        private void AddInvoiceOperation(int invoiceId, string type, IncomeModel income)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var invoiceOperation = new InvoiceOperation();
                    invoiceOperation.InvoiceId = invoiceId;
                    invoiceOperation.Type = type;
                    invoiceOperation.Amount = income.Amount;

                    var stage = GetCorrectStage(income.StageName);
                    invoiceOperation.Stage = stage.StageDate;
                    invoiceOperation.Year = stage.StageYear;
                    invoiceOperation.Month = stage.StageMonthName;

                    _context.Add(invoiceOperation);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException as SqlException;
                    if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                    {
                        Console.WriteLine($"Операция [{income.InvoiceName}] на сумму [{income.Amount}] Р. уже загружена в систему. \r\n");
                    }
                    else
                    {
                        Console.WriteLine($"Операция [{income.InvoiceName}] на сумму [{income.Amount}] Р. вызвала ошибку. \r\n");
                        Console.WriteLine(ex.Message);
                    }
                    transaction.Rollback();
                    RollBack();
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    RollBack();
                }
            }
        }

        private void RollBack()
        {
            var changedEntries = _context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        private Stage GetCorrectStage(string stageFrom1c)
        {
            var stageParts = stageFrom1c.Split(' ');
            if (stageParts.Length != 2)
                throw new System.Exception("Неверный формат периода. Правильный формат - месяц год");
            var month = stageParts[0];
            var year = int.Parse(stageParts[1]);
            return _stagesInDb
                .FirstOrDefault(x => 
                    x.StageMonthName.IndexOf(month, StringComparison.OrdinalIgnoreCase) >= 0 
                    && x.StageYear == year);
        }
    }
}
