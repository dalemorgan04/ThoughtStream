using System;
using System.Collections.Generic;
using Tasks.Service.Habits.Dto;

namespace Tasks.Service.Habits
{
    public interface IHabitService
    {
        /// <summary>
        /// Returns all habits
        /// </summary>
        /// <returns>List of HabitDtos</returns>
        List<HabitDto> GetHabits();
        /// <summary>
        /// Returns all habits on given day
        /// </summary>
        /// <param name="date">Specify day</param>
        /// <returns>List of HabtiDtos</returns>
        List<HabitDto> GetHabits(DateTime date);
    }
}
