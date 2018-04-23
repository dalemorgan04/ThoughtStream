using System.Collections.Generic;
using Tasks.Service.Tasks.Dto;
using Tasks.Service.Thoughts.Dto;
using Tasks.Service.Users.Dto;

namespace Tasks.ViewModels.Thoughts
{
    public class ThoughtsViewModel
    {        
        public UserDto User { get; set; }
        public List<ThoughtViewModel> ThoughtsList { get; set; }  
        public ThoughtViewModel Thought{ get; set; }
    }
}