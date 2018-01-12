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
        private readonly ISpecificationRepository<Task, int> taskRepository;
        private readonly ISpecificationRepository<Habit, int> habitRepository;
        private readonly IHabitRepository habitSqlRepository;

        public PlanWeekService(
            ISpecificationRepository<Task, int> taskRepository,
            ISpecificationRepository<Habit, int> habitRepository,
            IHabitRepository habitSqlRepository)
        {
            this.taskRepository = taskRepository;
            this.habitRepository = habitRepository;
            this.habitSqlRepository = habitSqlRepository;
        }

        public Dictionary<DayOfWeek, ItemListDto> GetCurrentWeekItems()
        {
            return GetWeekItems(DateTime.Now.StartOfWeek(DayOfWeek.Monday));
        }

        public Dictionary<DayOfWeek, ItemListDto> GetWeekItems(DateTime weekCommencingDate)
        {
            Dictionary<DayOfWeek, ItemListDto> weekList = new Dictionary<DayOfWeek, ItemListDto>(); //Each day will be a new list

            /* Pulls the next 7 days after the given date
             * Allowing flexibility in choosing the commence date
             * for the possibility of the user setting when 
             * the week start date could be i.e Monday or Sunday 
             */
            
            for (int i = 0; i < 7; i++)
            {
                DateTime date = weekCommencingDate.AddDays(i);
                weekList.Add(date.DayOfWeek, GetDayItems(date));
            }
            return weekList;
        }

        public Dictionary<TimeFrameType, ItemListDto> GetCurrentOpenItems()
        {
            return GetOpenItems(DateTime.Today);
        }

        public Dictionary<TimeFrameType, ItemListDto> GetOpenItems(DateTime date)
        {
            ItemListDto allItems = GetDayItems(date);
            Dictionary<TimeFrameType, ItemListDto> openItemLists = new Dictionary<TimeFrameType, ItemListDto>();

            //Week
            ItemListDto weekItems = new ItemListDto()
            {
                ItemDtos = new List<ItemDto>()
            };
            //Month
            ItemListDto monthItems = new ItemListDto()
            {
                ItemDtos = new List<ItemDto>()
            };
            //Year
            ItemListDto yearItems = new ItemListDto()
            {
                ItemDtos = new List<ItemDto>()
            };

            //Open
            ItemListDto openItems = new ItemListDto()
            {
                ItemDtos = new List<ItemDto>()
            };

            return openItemLists;
        }

        public ItemListDto GetDayItems(DateTime date)
        {
            ItemListDto itemList = new ItemListDto()
            {
                ItemDtos = new List<ItemDto>()
            };

            //Tasks
            List<Task> tasks = taskRepository
                                        .Find( new TaskOnDaySpec(date))
                                        .ToList();
            foreach (Task task in tasks)
            {
                ItemDto item = new ItemDto()
                {
                    Id = task.Id,
                    Description = task.Description,
                    Type = "Task"
                };
                itemList.ItemDtos.Add(item);
            }

            //Habits
            List<int> habitIds = habitSqlRepository.GetHabitOccurrencesOnDate(date);
            List<Habit> habits = habitRepository.Get(habitIds).ToList();
            foreach (Habit habit in habits)
            {
                ItemDto item = new ItemDto()
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