using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Service.Projects.Dto;

namespace Tasks.Service.Projects
{
    public interface IProjectService
    {
        List<ProjectDto> GetRootProjects();
    }
}