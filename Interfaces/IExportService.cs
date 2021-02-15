using System;

namespace SL
{
    public interface IExportService
    {
        void FillExportTable(DateTime stage, bool force);
    }
}