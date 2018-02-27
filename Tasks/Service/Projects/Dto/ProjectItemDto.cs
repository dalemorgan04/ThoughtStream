using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Enum;

namespace Tasks.Service.Projects.Dto
{
    public class ProjectItemDto
    {
        public int Id { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
    }
}