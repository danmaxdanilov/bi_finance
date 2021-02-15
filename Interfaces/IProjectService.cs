using System.Collections.Generic;
using DAL.WSLawyer;

namespace SL
{
    public interface IProjectService
    {
        List<Project> GetProjects();
    }
}