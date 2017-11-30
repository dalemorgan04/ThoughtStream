using System;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Habits.Entity
{
    public class HabitException : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual int HabitRecurrenceId { get; set; }
        public virtual DateTime Date { get; set; }
    }
}