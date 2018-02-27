using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository.Habits
{
    public class HabitExceptionMap : ClassMap<HabitException>
    {
        public HabitExceptionMap()
        {
            Table("HabitExceptions");
            Id(x => x.Id).Column("HabitExceptionsId").GeneratedBy.Native();
            References(x => x.HabitRecurrence).Column("HabitRecurrenceId");
            References(x => x.Habit).Column("HabitId");
            Map(x => x.Date);
            Map(x => x.IsHidden);
        }
    }
}