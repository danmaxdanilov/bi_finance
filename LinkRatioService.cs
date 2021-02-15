using System;
using System.Collections.Generic;
using System.Text;

namespace SL
{
    public class LinkRatioService : ILinkRatioService
    {
        private readonly INeo4jService _neo4JService;
        public LinkRatioService(INeo4jService neo4JService)
        {
            _neo4JService = neo4JService;
        }

        public void DeleteLinksFromDecreeEmployee()
        {
            _neo4JService.DeleteChildLinksFromDecreeEmployee();
        }

        public void FillLinkRatio()
        {
            _neo4JService.SetEmptyPaymentForOfficeAndDepartment();
            _neo4JService.FillRatioBetweenPaymentAndOffice();
            _neo4JService.FillRatioBetweenBackOfficeAndDepartment();
            _neo4JService.FillRatioBetweenOfficeAndLawyer();
            _neo4JService.FillRatioBetweenDepartmentAndLawyer();
        }
    }
}
