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
        private readonly IUnitOfWork unitOfWork;

        public ThoughtService(
            ISpecificationRepository<Thought, int> thoughtRepository,
            IThoughtRepository thoughtSqlRepository,
            IUnitOfWork unitOfWork)
        {
            this.thoughtRepository = thoughtRepository;
            this.thoughtSqlRepository = thoughtSqlRepository;
            this.unitOfWork = unitOfWork;
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
            Thought thought;
            if (thoughtDto.Id > 0)
            {
                thought = this.thoughtRepository.Get(thoughtDto.Id);
                thought.Update(
                    thoughtDto.Description, 
                    (int)thoughtDto.TimeFrame.TimeFrameType, 
                    thoughtDto.TimeFrame.TimeFrameDateTime, 
                    thoughtDto.Project );
            }
            else
            {
                thought = Thought.Create(thoughtDto.Description, thoughtDto.SortId, (int)thoughtDto.TimeFrame.TimeFrameType, thoughtDto.TimeFrame.TimeFrameDateTime, thoughtDto.Project );
                thoughtRepository.Add(thought);
            }
            this.unitOfWork.Commit();
        }

        public void Delete(int thoughtId)
        {
            thoughtRepository.Remove(thoughtId);
        }
    }
}