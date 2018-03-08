using AutoMapper;
using Tasks.Models.DomainModels.CalendarEvents.Entity;
using Tasks.Service.CalendarEvents.Dto;

namespace Tasks.Service.CalendarEvents
{
    public class CalendarEventDtoMap: Profile
    {
        public CalendarEventDtoMap()
        {
            CreateMap<CalendarEvent, CalendarEventDto>();
            CreateMap<CalendarEventDto, CalendarEvent>();
        }
    }
}