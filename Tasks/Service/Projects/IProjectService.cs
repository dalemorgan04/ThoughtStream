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
        ProjectWithItemsDto GetProject(int projectId);
        List<ProjectWithItemsDto> GetRootProjects();
        void AppendTask(int projectId, int taskId);
        void Save(ProjectWithItemsDto projectWithItemsDto);
        void Save(ProjectDto projectDto);
    }
}