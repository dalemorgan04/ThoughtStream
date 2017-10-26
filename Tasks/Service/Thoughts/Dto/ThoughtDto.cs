using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;

namespace Tasks.Service.Thoughts.Dto
{
    public class ThoughtDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int SortId { get; set; }
    }
}