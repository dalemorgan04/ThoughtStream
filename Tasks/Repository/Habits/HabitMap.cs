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
            Map(x => x.TimeFrameId);
            Map(x => x.DateTime);
            Map(x => x.Count);
            Map(x => x.IsComplete);
            Map(x => x.EndOnDate);
            Map(x => x.EndOnCount);
            Map(x => x.EndOnType);
            Map(x => x.Project);

            HasMany(x => x.HabitRecurrences).KeyColumn("HabitId");
        }
    }
}