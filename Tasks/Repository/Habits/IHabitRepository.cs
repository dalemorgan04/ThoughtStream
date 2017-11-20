using System;
using System.Collections.Generic;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository.Habits
{
    public interface IHabitRepository
    {
        List<int> GetHabitOccurrencesOnDate(DateTime date);
        List<HabitList> GetHabitOccurrencesBetweenDates(DateTime fromDate, DateTime toDate);        
    }
}
