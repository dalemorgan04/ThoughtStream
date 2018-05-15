using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Service.Tasks.Dto;

namespace Tasks.ViewModels.Tasks
{
    public class TasksViewModel
    {
        public List<TaskViewModel> TaskList { get; set; }
        public TaskViewModel EditViewModel { get; set; }
    }
}