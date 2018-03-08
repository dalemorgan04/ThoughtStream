using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tasks.Models.DomainModels.CalendarEvents.Entity;
using Tasks.Repository.Core;
using Tasks.Service.CalendarEvents.Dto;

namespace Tasks.Service.CalendarEvents
{
    public class CalendarEventService : ICalendarEventService
    {
        private readonly ISpecificationRepository<CalendarEvent, int> eventRepository;
        public CalendarEventService(ISpecificationRepository<CalendarEvent, int> eventRepository)
        {
            this.eventRepository = eventRepository;
        }
        public List<CalendarEventDto> GetCalendarEvents()
        {
            List<CalendarEvent> events = eventRepository.GetAll().ToList();
            return Mapper.Map<List<CalendarEvent>, List<CalendarEventDto>>(events);
        }

        public CalendarEventDto GetEvent(int calendarEventId)
        {
            CalendarEvent calendarEvent = eventRepository.Get(calendarEventId);
            return Mapper.Map<CalendarEvent, CalendarEventDto>(calendarEvent);
        }
    }
}