using System;
using System.Collections.Generic;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Habits.Entity
{
    public class HabitRecurrence : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual Habit Habit { get; set; }
        public virtual IntervalType IntervalType { get; set; }
        public virtual int RepeatEveryCount { get; set; }
        public virtual int DayOfWeek { get; set; }
        public virtual int DayOfMonth { get; set; }
        public virtual int WeekdayOfMonth { get; set; }
        public virtual int WeekOfYear { get; set; }
        public virtual int MonthNo { get; set; }
        public virtual DateTime StartOnDate { get; set; }

        public virtual IList<HabitException> HabitExceptions { get { return habitExceptions; } }
        protected IList<HabitException>  habitExceptions = new List<HabitException>();
    }
}