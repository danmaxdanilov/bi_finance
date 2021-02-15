using System;
using Microsoft.AspNetCore.Http;

namespace SL
{
    public interface IMenagementAccountingService
    {
        string BuildEngine(IFormFileCollection fileCollection);
        string ExportReport(DateTime stage, bool force);
        string ImportRevenue(IFormFileCollection fileCollection);
        string ImportIncome(IFormFileCollection fileCollection);
    }
}