using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Models.DomainModels;
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
            viewModel.EditViewModel = new TaskEditViewModel();
            return View("Index",viewModel);
        }

        [HttpPost]
        public ActionResult GetAddTask()
        {
            TaskEditViewModel viewModel = new TaskEditViewModel();
            return PartialView("_AddTask", viewModel);
        }

        [HttpPost]
        public ActionResult GetTasksTable()
        {
            TasksViewModel viewModel = new TasksViewModel();
            List<TaskDto> taskList = taskService.GetTasks().ToList();
            viewModel.TaskList = taskList;
            return PartialView("_TasksTable", viewModel);
        }

        [HttpPost]
        public bool Create(TaskEditViewModel viewModel)
        {
            TaskDto taskDto  = new TaskDto();
            taskDto.User = new User(){Id = 1};
            taskDto.Description = viewModel.Description;
            taskDto.Priority.Id = viewModel.PriorityId;
            taskDto.TimeFrame.TimeFrameId = viewModel.TimeFrameId;
            taskDto.TimeFrame.DateTime = viewModel.DateTime;
            taskService.Save(taskDto);
            return true;
        }
    }
}