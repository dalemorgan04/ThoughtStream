using System;
using System.Collections.Generic;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Habits.Entity
{
    public class Habit : IDomainEntity<int>
    {        
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual int TimeFrameId { get; set; }        
        public virtual TimeSpan Time { get; set; }
        public virtual IList<HabitRecurrence> HabitRecurrences
        {
            get { return HabitRecurrences; }
        }
        protected IList<HabitRecurrence> habitRecurrences = new List<HabitRecurrence>();
    }
}