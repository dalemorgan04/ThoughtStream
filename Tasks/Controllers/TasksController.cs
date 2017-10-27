using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Service.Tasks;
using Tasks.Service.Tasks.Dto;
using Tasks.ViewModels.Tasks;

namespace Tasks.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService taskService;

        public TasksController(
            ITaskService taskService)
        {
            this.taskService = taskService;
        }
        
        public ActionResult Index()
        {
            TasksViewModel viewModel = new TasksViewModel();
            List<TaskDto> taskList = taskService.GetTasks().ToList();
            viewModel.TaskList = taskList;
            return View("Index",viewModel);
        }
    }
}