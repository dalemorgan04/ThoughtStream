using System;
using System.Dynamic;
using Tasks.Models.DomainModels;

namespace Tasks.Service.Tasks.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }  
        public bool IsComplete { get; set; }
        public TimeFrame TimeFrame { get; set; }
    }
}