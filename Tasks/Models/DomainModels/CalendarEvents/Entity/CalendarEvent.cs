using System;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.CalendarEvents.Entity
{
    public class CalendarEvent : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual bool HasTime { get; set; }
        public virtual Project Project { get; set; }
    }
}