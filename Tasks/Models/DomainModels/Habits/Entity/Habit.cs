using System;
using System.Collections.Generic;
using Tasks.Models.DomainModels.Enum;
using Tasks.Models.DomainModels.Projects.Entity;
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
        public virtual DateTime DateTime {get; set;}
        public virtual int Count { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual DateTime EndOnDate { get; set; }
        public virtual int EndOnCount { get; set; }
        public virtual HabitEndOnType EndOnType { get; set; }
        public virtual Project Project { get; set; }

        public virtual IList<HabitRecurrence> HabitRecurrences { get { return habitRecurrences; } }
        protected IList<HabitRecurrence> habitRecurrences = new List<HabitRecurrence>();
        
    }
}