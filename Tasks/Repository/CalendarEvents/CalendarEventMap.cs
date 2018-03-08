using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels.CalendarEvents.Entity;

namespace Tasks.Repository.CalendarEvents
{
    public class CalendarEventMap : ClassMap<CalendarEvent>
    {
        public CalendarEventMap()
        {
            Table("Events");
            Id( x=> x.Id).Column("CalendarEventId").GeneratedBy.Native();
            References(x => x.User);
            Map(x => x.Description);
            References(x => x.Priority);
            Map(x => x.DateTime);
            Map(x => x.HasTime);
            References(x => x.Project);
        }
    }
}