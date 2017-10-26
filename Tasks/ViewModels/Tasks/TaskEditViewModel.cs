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
        public DateTime Due { get; set; }
        public Priority Priority { get; set; }
    }
}