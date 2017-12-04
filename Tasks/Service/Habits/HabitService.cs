using System;
using System.Collections.Generic;
using Tasks.Repository.Core;
using Tasks.Models.DomainModels.Habits.Entity;
using Tasks.Repository.Habits;
using Tasks.Service.Habits.Dto;
using System.Linq;
using AutoMapper;

namespace Tasks.Service.Habits
{
    public class HabitService : IHabitService
    {
        private readonly ISpecificationRepository<Habit, int> habitRepository;
        private readonly IHabitRepository habitSqlRepository;
        public HabitService(
            ISpecificationRepository<Habit, int> habitRepository,
            IHabitRepository habitSqlRepository)
        {
            this.habitRepository = habitRepository;
            this.habitSqlRepository = habitSqlRepository;
        }
        public List<HabitDto> GetHabits()
        {
            List<Habit> habits = habitRepository.GetAll().ToList();            
            return Mapper.Map<List<Habit>,List<HabitDto>>(habits);            
        }
        
        public List<HabitDto> GetHabitsOnDay(DateTime date)
        {
            var result = habitSqlRepository.GetHabitOccurrencesBetweenDates(new DateTime(2017, 11, 1), DateTime.Now);
            return new List<HabitDto>();
        }
    }
}