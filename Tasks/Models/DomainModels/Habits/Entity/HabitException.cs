using System;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Habits.Entity
{
    public class HabitException : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual HabitRecurrence HabitRecurrence { get; set; }
        public virtual Habit Habit { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool IsHidden { get; set; }
    }
}