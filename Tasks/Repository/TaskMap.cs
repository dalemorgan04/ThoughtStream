using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models;
using Tasks.Models.DomainModels;

namespace Tasks.Repository
{
    public class TaskMap: ClassMap<Task>
    {
        public TaskMap()
        {
            Table("Tasks");
            Id(x => x.Id).Column("TaskId").GeneratedBy.Native();
            References(x => x.User).Column("UserId");                
            Map(x => x.Description);
            References(x => x.Priority).Column("PriorityId");
            Component(x => x.Due, m =>
            {
                m.Map(x => x.Id).Column("TimeFrameId");
                m.Map(x => x.Time);
                m.Map(x => x.Date);
                m.Map(x => x.WeekCommencing);
                m.Map(x => x.Month);
                m.Map(x => x.Year);
            });
        }
    }
}