using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Habits.Entity
{
    public class HabitGoal : IDomainEntity<int>
    {
        public virtual int Id { get; set; } //ProjectId
        public virtual Habit Habit { get; set; }
        public virtual int CountRequired { get; set; }
        public virtual int Count { get; set; }
        public virtual string Description { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual int TimeFrameId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual Project Project { get; set; }
    }
}