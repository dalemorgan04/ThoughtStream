using System;

namespace Tasks.ViewModels.Thoughts
{
    public class ThoughtEditViewModel
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime Due { get; set; }
        public int PriorityId { get; set; }

        public ThoughtEditViewModel()
        {
        }
    }
}