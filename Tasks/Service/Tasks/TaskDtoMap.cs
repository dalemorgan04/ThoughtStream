using AutoMapper;
using Tasks.Models.DomainModels;
using Tasks.Service.Tasks.Dto;

namespace Tasks.Service.Tasks
{
    public class TaskDtoMap : Profile
    {
        public TaskDtoMap()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
    }
}