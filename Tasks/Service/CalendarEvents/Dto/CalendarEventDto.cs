using System;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Projects.Entity;

namespace Tasks.Service.CalendarEvents.Dto
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime DateTime { get; set; }
        public bool HasTime { get; set; }
        public Project Project { get; set; }
    }
}