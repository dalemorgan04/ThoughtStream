using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Habits.Entity;
using Tasks.Models.DomainModels.Tasks.Spec;
using Tasks.Repository.Core;
using Tasks.Repository.Habits;
using Tasks.Service.PlanWeek.Dto;

namespace Tasks.Service.PlanWeek
{
    public class PlanWeekService : IPlanWeekService
    {
        ISpecificationRepository<Task, int> taskRepository;
        ISpecificationRepository<Habit, int> habitRepository;
        IHabitRepository habitSqlRepository;

        public PlanWeekService(
            ISpecificationRepository<Task, int> taskRepository,
            ISpecificationRepository<Habit, int> habitRepository,
            IHabitRepository habitSqlRepository)
        {
            this.taskRepository = taskRepository;
            this.habitRepository = habitRepository;
            this.habitSqlRepository = habitSqlRepository;
        }

        public WeekPlanDto GetCurrentWeekPlan()
        {
            WeekPlanDto weekPlan = new WeekPlanDto()
            {
                DayPlans = new List<DayPlanDto>()
            };
            for ( int i = 0; i < 7 ;i++  )
            {
                DateTime date = DateTime.Now.AddDays(i);
                weekPlan.DayPlans.Add(GetDayPlan(date));
            }
            return weekPlan;
        }

        private DayPlanDto GetDayPlan(DateTime date)
        {
            DayPlanDto dayPlan = new DayPlanDto()
            {
                Items = new List<DayPlanItemDto>()
            };

            //Tasks
            List<Task> tasks = taskRepository
                                        .Find( new TaskOnDaySpec(date))
                                        .ToList();
            foreach (Task task in tasks)
            {
                DayPlanItemDto item = new DayPlanItemDto()
                {
                    Id = task.Id,
                    Description = task.Description,
                    Type = "Task"
                };
                dayPlan.Items.Add(item);
            }

            //Habits
            List<int> habitIds = habitSqlRepository.GetHabitOccurrencesOnDate(date);
            List<Habit> habits = habitRepository.Get(habitIds).ToList();
            foreach (Habit habit in habits)
            {
                DayPlanItemDto item = new DayPlanItemDto()
                {
                    Id = habit.Id,
                    Description = habit.Description,
                    Type = "Habit"
                };
                dayPlan.Items.Add(item);
            }

            return dayPlan;
        }
    }
}