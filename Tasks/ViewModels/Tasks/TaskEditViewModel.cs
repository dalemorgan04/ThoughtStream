using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Models.DomainModels;

namespace Tasks.ViewModels.Tasks
{
    public class TaskEditViewModel
    {
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public TimeFrameType TimeFrameId { get; set; }
        public DateTime DateTime { get; set; }
        
        public List<SelectListItem> PriorityDropDownItems { get; set; }


        public TaskEditViewModel()
        {
            Description = "";
            PriorityId = 6;
            TimeFrameId = TimeFrameType.Date;
            DateTime = DateTime.Today;
        }
    }
}