using System;
using Tasks.Models.DomainModels;

namespace Tasks.Service.Tasks.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Due Due { get; set; }
    }
}