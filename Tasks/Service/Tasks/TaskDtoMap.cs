using System.Drawing.Drawing2D;
using System.Web.UI;
using AutoMapper;
using Tasks.Models.DomainModels;
using Tasks.Service.Tasks.Dto;

namespace Tasks.Service.Tasks
{
    public class TaskDtoMap : Profile
    {
        public TaskDtoMap()
        {
            CreateMap<Task, TaskDto>()
                .ForMember(dest => dest.TimeFrame, input => input.MapFrom(i => new TimeFrame(i.TimeFrameId, i.DateTime)));
            CreateMap<TaskDto, Task>()
                .ForMember(dest => dest.TimeFrameId, input => input.MapFrom(t => t.TimeFrame.TimeFrameId));                
        }
    }
}