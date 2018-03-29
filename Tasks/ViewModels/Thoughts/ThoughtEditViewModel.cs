using System;
using Tasks.Models.DomainModels;

namespace Tasks.ViewModels.Thoughts
{
    public class ThoughtEditViewModel
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int SortId { get; set; }
        public int PriorityId { get; set; }

        public int TimeFrameId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string WeekString { get; set; }
        public string DueString { get; set; }
    }
}