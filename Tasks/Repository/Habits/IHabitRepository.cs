using System;
using System.Collections.Generic;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.Repository.Habits
{
    public interface IHabitRepository
    {
        /// <summary>
        /// Get habit occurences on specific date
        /// </summary>
        /// <param name="date">specific date</param>
        /// <returns>List of HabitIds as ints</returns>
        List<int> GetHabitOccurrences(DateTime date);
        /// <summary>
        /// Get habit occurences between two dates
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>List of HabitIds as ints</returns>
        List<HabitList> GetHabitOccurrences(DateTime fromDate, DateTime toDate);        
    }
}
