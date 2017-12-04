using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Habits.Dto;

namespace Tasks.Service.Habits
{
    public interface IHabitService
    {
        List<HabitDto> GetHabits();
        List<HabitDto> GetHabitsOnDay(DateTime date);
    }
}
