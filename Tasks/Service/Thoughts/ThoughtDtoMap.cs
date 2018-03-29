using AutoMapper;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Thoughts;
using Tasks.Models.DomainModels.Thoughts.Entity;
using Tasks.Service.Thoughts.Dto;

namespace Tasks.Service.Thoughts
{
    public class ThoughtDtoMap : Profile
    {
        public ThoughtDtoMap()
        {
            CreateMap<Thought, ThoughtDto>()
                .ForMember(dest => dest.TimeFrame,
                    input => input.MapFrom(i => new TimeFrame((TimeFrameType) i.TimeFrameId, i.DateTime)));
            CreateMap<ThoughtDto, Thought>()
                .ForMember(dest => dest.TimeFrameId,
                    input => input.MapFrom(t => (int) t.TimeFrame.TimeFrameType))
                .ForMember(dest => dest.DateTime,
                    input => input.MapFrom(t => t.TimeFrame.DateTime));
        }
    }
}