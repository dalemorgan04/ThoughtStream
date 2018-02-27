using System;

namespace Tasks.Models.DomainModels.Habits.Entity
{
    public class HabitList
    {
        //Used as model to be returned by stored procedures when querying habits on a given date
        public DateTime Date { get; set; }
        public int HabitId { get; set; }
    }
}