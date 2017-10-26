using System;

namespace Tasks.Models.DomainModels
{
    public class Habit
    {
        public virtual int HabitId { get; set; }
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Due { get; set; }
        public virtual Priority Priority { get; set; }
    }
}