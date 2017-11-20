using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository
{
    public class HabitMap: ClassMap<Habit>
    {
        public HabitMap()
        {
            Table("Habits");
            Id(x => x.Id).Column("HabitId").GeneratedBy.Native();
            References(x => x.User).Column("UserId");
            Map(x => x.Description);
            References(x => x.Priority).Column("PriorityId");
            HasMany(x => x.HabitRecurrences).KeyColumn("HabitId");
        }
    }
}