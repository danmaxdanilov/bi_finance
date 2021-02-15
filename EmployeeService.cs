using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IExcelMethods _excelMethods;
        private readonly INeo4jService _neo4JService;
        public EmployeeService(IExcelMethods excelMethods, INeo4jService neo4JService)
        {
            _excelMethods = excelMethods;
            _neo4JService = neo4JService;
        }

        public void FillEmployee(Stream stream)
        {
            var employees = _excelMethods.GetEmployeesFromFile(stream);
            employees.ForEach(x => _neo4JService.CreateEmployee(x));
            _neo4JService.CreateBackOffice();
            _neo4JService.CreateLinkFromOfficeToLawyer();
            _neo4JService.CreateOffice();
        }
    }
}
