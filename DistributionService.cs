using DAL.WSLawyer;
using System.IO;
using System.Threading;

namespace SL
{
    public class DistributionService : IDistributionService
    {
        private readonly IExcelMethods _excelMethods;
        private readonly INeo4jService _neo4JService;
        private readonly IProjectService _projectService;
        public DistributionService(IExcelMethods excelMethods, INeo4jService neo4JService, IProjectService projectService)
        {
            _excelMethods = excelMethods;
            _neo4JService = neo4JService;
            _projectService = projectService;
        }

        public void FillLawyerDistribution(Stream file)
        {
            FillProfiCenterFromProject();
            Thread.Sleep(100);
            var distributions = _excelMethods.GetLawyerDistributionFromFile(file);
            distributions.ForEach(distribution =>
            {
                foreach (var item in distribution.Distribution)
                {
                    if (item.Value > 0)
                        _neo4JService.CreateLinkArcProject(distribution.Fio, item.Key, item.Value);
                }
            });
            _neo4JService.CreateLinkArcProjectMka();
        }

        private void FillProfiCenterFromProject()
        {
            var projects = _projectService.GetProjects();
            var otherProject = new Project { Id = 777, Name = "Иные проекты"};
            projects.Add(otherProject);
            projects.ForEach(p => _neo4JService.CreateProfitCentersFromProjects(int.Parse(p.Id.ToString()), p.Name));
        }
    }
}
