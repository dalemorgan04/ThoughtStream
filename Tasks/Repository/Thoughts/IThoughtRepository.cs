using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasks.Repository.Thoughts
{
    public interface IThoughtRepository
    {
        void UpdateSortId(int thoughtId, int moveToId);
    }
}