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
            DateTime timeFrameDate = new DateTime(viewModel.Date.Year, viewModel.Date.Month, viewModel.Date.Day);
            DateTime timeFrameDateTime = timeFrameDate.Date.Add( viewModel.HasTime ? new TimeSpan(0, 0, 0) : viewModel.Time);

            TaskDto taskDto = new TaskDto()
            {
                Description = viewModel.Description,
                Priority = new Priority() { Id = viewModel.PriorityId },
                TimeFrame = new TimeFrame((TimeFrameType)viewModel.TimeFrameId,timeFrameDateTime),
                DateTime = timeFrameDateTime,
                User = new User() { Id = 1}
            };
            
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
                TabList = new List<Tab>()
                {
                    new Tab()
                    {
                        Name = "Add",
                        OrderNumber = 1,
                        IsDefault = true,
                        IsEnabled = true
                    },
                    new Tab()
                    {
                        Name = "Edit",
                        OrderNumber = 1,
                        IsDefault = false,
                        IsEnabled = false
                    }
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
                Description = "",
                TimeFrameId = TimeFrameType.Date,
                PriorityId = 0,
                PriorityDropDownItems = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Low", Value = "1" , Selected = true},
                    new SelectListItem(){Text = "Medium", Value = "2"},
                    new SelectListItem(){Text = "High", Value = "3"}
                }
            };
            return PartialView("_AddTask", viewModel);
        }

        public ActionResult GetAsideEditTab(int taskId)
        {
            TaskDto task =  taskService.GetTaskById(taskId);
            DateTime taskDate = new DateTime(task.DateTime.Year, task.DateTime.Month, task.DateTime.Day);
            TimeSpan taskTime =  ((TimeFrameType)task.TimeFrame.TimeFrameId == TimeFrameType.Time) ? new TimeSpan( task.DateTime.Hour, task.DateTime.Minute, 0) : new TimeSpan(0, 0, 0);
            
            TaskEditViewModel viewModel = new TaskEditViewModel
            {
                //Defaults
                Date = taskDate.Add(taskTime),
                HasTime = false,
                Time = taskTime,
                Description = "",
                TimeFrameId = TimeFrameType.Date,
                PriorityId = 0,
                PriorityDropDownItems = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Low", Value = "1" , Selected = true},
                    new SelectListItem(){Text = "Medium", Value = "2"},
                    new SelectListItem(){Text = "High", Value = "3"}
                }
            };
            return PartialView("");
        }

        public ActionResult GetAsideDragTab()
        {
            return PartialView("");
        }
    }
}