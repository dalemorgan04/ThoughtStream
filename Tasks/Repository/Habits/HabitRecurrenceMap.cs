using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository
{
    public class HabitRecurrenceMap : ClassMap<HabitRecurrence>
    {
        public HabitRecurrenceMap()
        {
            Table("HabitRecurrences");
            Id(x => x.Id).Column("RecurrenceId").GeneratedBy.Native();
            References(x => x.Habit).Column("HabitId");            
            Map(x => x.IntervalType);
            Map(x => x.RepeatEveryCount);
            Map(x => x.DayOfWeek);
            Map(x => x.DayOfMonth);
            Map(x => x.WeekdayOfMonth);
            Map(x => x.WeekOfYear);
            Map(x => x.MonthNo);            
            Map(x => x.StartOnDate);
        }
    }
}