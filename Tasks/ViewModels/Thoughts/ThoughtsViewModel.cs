using System.Collections.Generic;
using Tasks.Service.Tasks.Dto;
using Tasks.Service.Thoughts.Dto;
using Tasks.Service.Users.Dto;

namespace Tasks.ViewModels.Thoughts
{
    public class ThoughtsViewModel
    {        
        public UserDto User { get; set; }
        public List<ThoughtDto> ThoughtList { get; set; }  
        public AddThoughtViewModel NewThought { get; set; }
    }
}