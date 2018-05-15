using System.Collections.Generic;
using Tasks.Service.Tasks.Dto;
using Tasks.Service.Thoughts.Dto;
using Tasks.Service.Users.Dto;

namespace Tasks.ViewModels.Thoughts
{
    public class ThoughtsViewModel
    {   
        public List<ThoughtViewModel> ThoughtList { get; set; }  
        public ThoughtViewModel EditViewModel{ get; set; }
    }
}