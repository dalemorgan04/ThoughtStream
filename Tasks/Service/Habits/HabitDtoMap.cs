using AutoMapper;
using Tasks.Models.DomainModels.Habits.Entity;
using Tasks.Service.Habits.Dto;

namespace Tasks.Service.Habits
{
    public class HabitDtoMap : Profile
    {
        public HabitDtoMap()
        {
            CreateMap<Habit, HabitDto>();
            CreateMap<HabitDto, Habit>();
        }        
    }
}