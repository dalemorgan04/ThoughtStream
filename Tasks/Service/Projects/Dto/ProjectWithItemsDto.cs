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
        public ItemType Type => ItemType.Project;
        public List<ProjectItemDto> Items { get; set; }

        //In the future potentially think about creating a tree
        //For now it is acceptable to view one level at a time and call the next level on demand
        //When that happens look at a recursive formula
    }


}