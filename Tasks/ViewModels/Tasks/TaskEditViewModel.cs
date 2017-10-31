using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;

namespace Tasks.ViewModels.Tasks
{
    public class TaskEditViewModel
    {
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public int TimeFrameId { get; set; }
        public DateTime DateTime { get; set; }

        public TaskEditViewModel()
        {
            Description = "";
            PriorityId = 6;
            TimeFrameId = 1;
            DateTime = DateTime.Today;
        }
    }
}