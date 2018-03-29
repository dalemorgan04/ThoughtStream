using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tasks.Repository.Core;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Thoughts;
using Tasks.Models.DomainModels.Thoughts.Entity;
using Tasks.Repository.Thoughts;
using Tasks.Service.Thoughts.Dto;

namespace Tasks.Service.Thoughts
{
    public class ThoughtService : IThoughtService
    {
        private readonly ISpecificationRepository<Thought, int> thoughtRepository;
        private readonly IThoughtRepository thoughtSqlRepository;

        public ThoughtService(
            ISpecificationRepository<Thought, int> thoughtRepository,
            IThoughtRepository thoughtSqlRepository)
        {
            this.thoughtRepository = thoughtRepository;
            this.thoughtSqlRepository = thoughtSqlRepository;
        }

        public IList<ThoughtDto> GetThoughts()
        {
            List<Thought> thoughtList = thoughtRepository.GetAll().ToList();
            List<ThoughtDto> thoughtDtoList = Mapper.Map<List<Thought>, List<ThoughtDto>>(thoughtList);
            return thoughtDtoList;
        }

        public ThoughtDto GetThoughtById(int id)
        {
            Thought thought = thoughtRepository.Get(id);
            ThoughtDto thoughtDto = Mapper.Map<Thought, ThoughtDto>(thought);
            return thoughtDto;
        }

        public void UpdateSortId(int thoughtId, int moveToSortId)
        {
            thoughtSqlRepository.UpdateSortId(thoughtId, moveToSortId);
        }

        public void Save(ThoughtDto thoughtDto)
        {
            Thought thought = Mapper.Map<ThoughtDto, Thought>(thoughtDto);
            thoughtRepository.Add(thought);
        }

        public void Delete(int thoughtId)
        {
            thoughtRepository.Remove(thoughtId);
        }
    }
}