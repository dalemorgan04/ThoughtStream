using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.Models.DomainModels;

namespace Tasks.ViewModels.Tasks
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public bool IsComplete { get; set; }
        public int TimeFrameId { get; set; }
        public string TimeFrameDateString { get; set; }
        public string TimeFrameTimeString { get; set; }
        public string TimeFrameDueString { get; set; }
    }
}