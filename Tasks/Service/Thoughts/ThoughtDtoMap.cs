using AutoMapper;
using Tasks.Models.DomainModels;
using Tasks.Service.Thoughts.Dto;

namespace Tasks.Service.Thoughts
{
    public class ThoughtDtoMap : Profile
    {
        public ThoughtDtoMap()
        {
            CreateMap<Thought, ThoughtDto>();
            CreateMap<ThoughtDto, Thought>();
        }
    }
}