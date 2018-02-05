using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Enum;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Tasks;
using Tasks.Service.Tasks.Dto;
using Tasks.ViewModels.Aside;
using Tasks.ViewModels.Tasks;

namespace Tasks.Controllers
{
    public class TasksController : BaseController, IAsideController
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
            //taskDto.TimeFrame.Id= viewModel.TimeFrameId;
            taskDto.DateTime = viewModel.DateTime;
            taskService.Save(taskDto);
            return true;
        }

        public bool Update(TaskEditViewModel viewModel)
        {
            return true;
        }

        public ActionResult GetDefaultAsideLayout()
        {
            var viewModel = new AsideViewModel()
            {
                VisibleTabsList = new List<Tab>()
                {
                    {new Tab(){ OrderNumber = 0, TabType = AsideTabType.Add, Name = "Selection"} }
                }
            };
            return PartialView("_Aside", viewModel);
        }

        public ActionResult GetDefaultAsideContent()
        {
            return GetAsideAddTab();
        }

        public ActionResult GetAsideAddTab()
        {
            TaskEditViewModel viewModel = new TaskEditViewModel
            {
                DateTime = DateTime.Now,
                Description = "Enter description",
                TimeFrameId = TimeFrameType.Date
            };
            return PartialView("_AddTask", viewModel);
        }

        public ActionResult GetAsideEditTab()
        {
            return PartialView("");
        }

        public ActionResult GetAsideDragTab()
        {
            return PartialView("");
        }
    }
}