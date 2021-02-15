using System;
using System.Collections.Generic;
using System.Text;

namespace SL
{
    public class CalculateService : ICalculateService
    {
        private readonly INeo4jService _neo4JService;
        private readonly IMathService _mathService;
        public CalculateService(INeo4jService neo4JService, IMathService mathService)
        {
            _neo4JService = neo4JService;
            _mathService = mathService;
        }

        public void SolveMatrix()
        {
            var matrixOfInitialCoefficients = _neo4JService.GetMatrixOfInitialCoefficients();
            var index = 0;
            _neo4JService.ResetIdentityToCostCenter();
            foreach (var arc in matrixOfInitialCoefficients)
            {
                _neo4JService.SetIdentityToCostCenter(arc.Key, index);
                index++;
            }
            double[][] Kexp = new double[matrixOfInitialCoefficients.Values.Count][];
            matrixOfInitialCoefficients.Values.CopyTo(Kexp, 0);
            double[] Zexp = _neo4JService.GetVectorOfDirectCost();
            
            var resultMatrix = _mathService.CalculateMatrix(Kexp, Zexp);

            _neo4JService.ResetAllIndirectCosts();

            var linkArcs = _neo4JService.GetExistedConnections();
            linkArcs.ForEach(linkArc =>
                _neo4JService.UpdateIndirectCost(
                    linkArc.SourceId, linkArc.DestinationId,
                    (decimal)resultMatrix[linkArc.DestinationId, linkArc.SourceId])
            );
        }
    }
}
