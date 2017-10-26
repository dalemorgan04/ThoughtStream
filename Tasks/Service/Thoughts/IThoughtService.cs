using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Service.Thoughts.Dto;

namespace Tasks.Service.Thoughts
{
    public interface IThoughtService
    {
        IList<ThoughtDto> GetThoughts();
        ThoughtDto GetThoughtById(int id);
        void Save(ThoughtDto thoughtDto);
        void Delete(int thoughtId);
        void UpdateSortId(int thoughtId, int moveToSortId);
    }
}