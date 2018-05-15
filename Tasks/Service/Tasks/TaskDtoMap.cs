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
                .ForMember(dest => dest.TimeFrame,
                    input => input.MapFrom(i => new TimeFrame((TimeFrameType) i.TimeFrameId, i.TimeFrameDateTime)));

            CreateMap<TaskDto, Task>()
                .ForMember(dest => dest.TimeFrameId,
                    input => input.MapFrom(t => t.TimeFrame.TimeFrameType))
                .ForMember(dest => dest.TimeFrameDateTime,
                    input => input.MapFrom(t => t.TimeFrame.TimeFrameDateTime));
        }
    }
}