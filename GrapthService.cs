using System;
using System.Collections.Generic;
using System.Text;

namespace SL
{
    public class GrapthService : IGrapthService
    {
        private readonly IMathService _mathService;
        private readonly INeo4jService _neo4JService;
        public GrapthService(IMathService mathService, INeo4jService neo4JService)
        {
            _mathService = mathService;
            _neo4JService = neo4JService;
        }

        public void CalculateGrapth()
        {
            double[][] Kexp;
            double[] Zexp;

            Kexp = _neo4JService.GetMatrixOfInitialCoefficients();
            Zexp = _neo4JService.GetVectorOfDirectCost();

            var resultMatrix = _mathService.CalculateMatrix(Kexp, Zexp);

            _neo4JService.ResetAllIndirectCosts();
            var linkArcs = _neo4JService.GetExistedConnections();
            linkArcs.ForEach(linkArc =>
                _neo4JService.UpdateIndirectCost(
                    linkArc.SourceId, linkArc.DestinationId,
                    (decimal)resultMatrix[linkArc.DestinationId - 1, linkArc.SourceId - 1])
            );
        }
    }
}
