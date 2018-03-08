using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Projects.Entity;

namespace Tasks.Repository.Projects
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Table("Projects");
            Id( x => x.Id).Column("ProjectId").GeneratedBy.Native();            
            References( x => x.User).Column("UserId");
            Map(x => x.Description);
            References(x => x.Priority).Column("PriorityId");
            Map(x => x.TimeFrameId);
            Map(x => x.DateTime);
            Map(x => x.IsComplete);
        }
    }
}