using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Enum;
using Tasks.Models.DomainModels.Projects.Entity;

namespace Tasks.Service.Projects.Dto
{
    public class ProjectWithItemsDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<ProjectItemDto> Items { get; set; }
        public List<ProjectWithItemsDto> SubProjects { get; set; }
        public ProjectWithItemsDto()
        {
            Items = new List<ProjectItemDto>();
            SubProjects = new List<ProjectWithItemsDto>();
        }
    }
}