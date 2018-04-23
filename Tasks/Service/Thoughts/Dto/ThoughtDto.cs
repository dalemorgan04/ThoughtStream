using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Projects.Entity;

namespace Tasks.Service.Thoughts.Dto
{
    public class ThoughtDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int SortId { get; set; }
        public TimeFrame TimeFrame { get; set; }
        public Project Project { get; set; }
    }
}