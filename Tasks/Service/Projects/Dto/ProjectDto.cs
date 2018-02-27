using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Projects.Entity;

namespace Tasks.Service.Projects.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public  User User { get; set; }
        public string Description { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual int TimeFrameId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual Project ParentProject { get; set; }
    }
}