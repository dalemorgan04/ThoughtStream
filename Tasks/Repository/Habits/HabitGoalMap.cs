using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository.Habits
{
    public class HabitGoalMap : ClassMap<HabitGoal>
    {
        public HabitGoalMap()
        {
            Table("HabitGoals");
            Id(x => x.Id).Column("HabitGoalId");            
            References(x => x.Habit).Column("HabitId");
            Map(x => x.CountRequired);
            Map(x => x.Count);
            Map(x => x.Description);
            References(x => x.Priority).Column("PriorityId");
            Map(x => x.TimeFrameId);
            Map(x => x.DateTime);
            Map(x => x.IsActive);
            Map(x => x.IsComplete);
            References(x => x.Project).Column("ProjectId");
        }
    }
}