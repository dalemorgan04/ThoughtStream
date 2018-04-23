using System;
using Tasks.Models.DomainModels;

namespace Tasks.ViewModels.Thoughts
{
    public class ThoughtViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int SortId { get; set; }
        public int PriorityId { get; set; }

        public int TimeFrameId { get; set; }
        public DateTime TimeFrameDate { get; set; }
        public TimeSpan TimeFrameTime { get; set; }
        public string TimeFrameWeekString { get; set; }
        public string TimeFrameDueString { get; set; }
    }
}