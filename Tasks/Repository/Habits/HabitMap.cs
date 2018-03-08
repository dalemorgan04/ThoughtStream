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
            
            References(x => x.Project).Column("ProjectId");
            
            HasMany(x => x.HabitRecurrences)
                .Access.CamelCaseField()
                .Cascade.All()
                .AsBag()
                .BatchSize(5)
                .Fetch.Subselect()
                .Not.KeyUpdate() // Prevents redundant update of DepotMailCentres after insert. https://stackoverflow.com/questions/11468668/nhibernate-still-issues-update-after-insert - JMB
                .KeyColumn("HabitId");
        }
    }
}