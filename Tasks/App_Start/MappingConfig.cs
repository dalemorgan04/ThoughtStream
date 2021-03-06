﻿using AutoMapper;
using Tasks.Service.CalendarEvents;
using Tasks.Service.Habits;
using Tasks.Service.Tasks;
using Tasks.Service.Thoughts;
using Tasks.Service.Users;

namespace Tasks
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                //Domain Model

                //Dtos
                cfg.AddProfile(new TaskDtoMap());
                cfg.AddProfile(new CalendarEventDtoMap());
                cfg.AddProfile(new UserDtoMap());
                cfg.AddProfile(new ThoughtDtoMap());
                cfg.AddProfile(new HabitDtoMap());

            });
        }
    }
}