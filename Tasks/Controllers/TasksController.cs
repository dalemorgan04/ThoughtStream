using System;
using System.Collections.Generic;
using System.Globalization;
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
                //Defaults
                Date = DateTime.Now,
                HasTime = false,
                Month = DateTime.Now.Month,
                Time = DateTime.Now.Date,
                WeekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now,CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                Description = "",
                TimeFrameId = TimeFrameType.Date,
                PriorityDropDownItems = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Low", Value = "1" , Selected = true},
                    new SelectListItem(){Text = "Medium", Value = "2"},
                    new SelectListItem(){Text = "High", Value = "3"}
                }
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