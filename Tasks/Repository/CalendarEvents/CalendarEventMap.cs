using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels.CalendarEvents.Entity;

namespace Tasks.Repository.CalendarEvents
{
    public class CalendarEventMap : ClassMap<CalendarEvent>
    {
        public CalendarEventMap()
        {
            Table("CalendarEvents");
            Id( x=> x.Id).Column("CalendarEventId").GeneratedBy.Native();
            References(x => x.User).Column("UserId");
            Map(x => x.Description);
            References(x => x.Priority).Column("PriorityId");
            Map(x => x.DateTime);
            Map(x => x.HasTime);
            References(x => x.Project).Column("ProjectId");
        }
    }
}