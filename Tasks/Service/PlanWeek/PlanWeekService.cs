using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Infrastructure.Extension;
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
        private readonly ISpecificationRepository<Habit, int> habitRepository;
        private readonly IHabitRepository habitSqlRepository;
        private readonly ISpecificationRepository<Task, int> taskRepository;

        public PlanWeekService(
            ISpecificationRepository<Task, int> taskRepository,
            ISpecificationRepository<Habit, int> habitRepository,
            IHabitRepository habitSqlRepository)
        {
            this.taskRepository = taskRepository;
            this.habitRepository = habitRepository;
            this.habitSqlRepository = habitSqlRepository;
        }

        public InWeekItemList GetCurrentWeekItems()
        {
            return GetWeekItems(DateTime.Now.StartOfWeek(DayOfWeek.Monday));
        }

        public InWeekItemList GetWeekItems(DateTime weekCommencingDate)
        {
            var weekList = new InWeekItemList(); //Each day will be a new list

            /* Pulls the next 7 days after the given date
             * Allowing flexibility in choosing the commence date
             * for the possibility of the user setting when
             * the week start date could be i.e Monday or Sunday
             */

            for (var i = 0; i < 7; i++)
            {
                var date = weekCommencingDate.AddDays(i);
                weekList.Update(date.DayOfWeek, GetDayItems(date));
            }
            return weekList;
        }

        public OpenItemList GetCurrentOpenItems()
        {
            return GetOpenItems(DateTime.Today);
        }

        public OpenItemList GetOpenItems(DateTime date)
        {
            
            var openItemList = new OpenItemList();

            //Week
            var weekItems = new ItemListDto
            {
                ItemDtos = new List<ItemDto>()
                {
                    {new ItemDto() {Id = 97, Description = "Week example", Type = "Week"}},
                    {new ItemDto() {Id = 98, Description = "Week example 2", Type = "Week"}},
                    {new ItemDto() {Id = 99, Description = "Week example 3", Type = "Week"}}
                }
            };
            
            openItemList.Update(TimeFrameType.Week, weekItems);

            //Month
            var monthItems = new ItemListDto
            {
                ItemDtos = new List<ItemDto>()
            };
            openItemList.Update(TimeFrameType.Month, weekItems);

            //Year
            var yearItems = new ItemListDto
            {
                ItemDtos = new List<ItemDto>()
            };
            openItemList.Update(TimeFrameType.Year, weekItems);

            //Open
            var openItems = new ItemListDto
            {
                ItemDtos = new List<ItemDto>()
            };
            openItemList.Update(TimeFrameType.Open, weekItems);

            return openItemList;
        }

        public ItemListDto GetDayItems(DateTime date)
        {
            var itemList = new ItemListDto();

            //Tasks
            var tasks = taskRepository
                .Find(new TaskOnDaySpec(date))
                .ToList();
            foreach (var task in tasks)
            {
                var item = new ItemDto
                {
                    Id = task.Id,
                    Description = task.Description,
                    Type = "Task"
                };
                itemList.ItemDtos.Add(item);
            }

            //Habits
            var habitIds = habitSqlRepository.GetHabitOccurrencesOnDate(date);
            var habits = habitRepository.Get(habitIds).ToList();
            foreach (var habit in habits)
            {
                var item = new ItemDto
                {
                    Id = habit.Id,
                    Description = habit.Description,
                    Type = "Habit"
                };
                itemList.ItemDtos.Add(item);
            }

            return itemList;
        }
    }
}