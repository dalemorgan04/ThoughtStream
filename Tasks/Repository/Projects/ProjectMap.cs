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
            Map(x => x.IsActive);
            Map(x => x.IsComplete);

            References(x => x.ParentProject).Column("ParentProjectId");

            HasMany(x => x.Tasks)
                .Access.CamelCaseField()
                .Cascade.All()                
                .AsBag()
                .KeyColumn("ProjectId");

            HasMany(x => x.HabitGoals)
                .Access.CamelCaseField()
                .Cascade.All()                
                .AsBag()
                .KeyColumn("ProjectId");                  
        }
    }
}