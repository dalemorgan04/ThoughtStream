using System.Collections.Generic;
using Tasks.Service.CalendarEvents.Dto;

namespace Tasks.Service.CalendarEvents
{
    public interface ICalendarEventService
    {
        List<CalendarEventDto> GetCalendarEvents();
    }
}