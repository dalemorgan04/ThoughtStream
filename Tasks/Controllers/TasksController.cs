using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels;
using Tasks.Service.Tasks;
using Tasks.Service.Tasks.Dto;
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
            List<TaskDto> taskList = taskService.GetTasks().ToList();
            TasksViewModel viewModel = new TasksViewModel()
            {
                TaskList = taskList,
                EditViewModel = new TaskEditViewModel()
            };
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
        
        
        //Aside

        public ActionResult GetAside()
        {
            return PartialView("_Aside");
        }

        public ActionResult GetAsideAddTab()
        {
            TaskEditViewModel viewModel = getDefaultAsideViewModel();
            return PartialView("_AddTask", viewModel);
        }

        public ActionResult GetAsideEditTab(int taskId)
        {
            if (taskId == 0)
            {
                //Default edit tab when nothing is selected
                TaskEditViewModel viewModel = getDefaultAsideViewModel();
                return PartialView("_EditTask", viewModel);
            }
            else
            {
                TaskDto task = taskService.GetTaskById(taskId);
                DateTime taskDate = new DateTime(task.DateTime.Year, task.DateTime.Month, task.DateTime.Day);
                TimeSpan taskTime = ((TimeFrameType) task.TimeFrame.TimeFrameType == TimeFrameType.Time)
                    ? new TimeSpan(task.DateTime.Hour, task.DateTime.Minute, 0)
                    : new TimeSpan(0, 0, 0);

                bool hasTime = ((TimeFrameType) task.TimeFrame.TimeFrameType == TimeFrameType.Time ||
                                (TimeFrameType) task.TimeFrame.TimeFrameType == TimeFrameType.Date)
                    ? true
                    : false;

                TaskEditViewModel viewModel = new TaskEditViewModel
                {
                    Id = task.Id,
                    Date = task.DateTime,
                    HasTime = hasTime,
                    Time = taskTime,
                    Description = task.Description,
                    TimeFrameId = (TimeFrameType) task.TimeFrame.TimeFrameType,
                    PriorityId = 0,
                    PriorityDropDownItems = new List<SelectListItem>()
                    {
                        new SelectListItem() {Text = "Low", Value = "1", Selected = true},
                        new SelectListItem() {Text = "Medium", Value = "2"},
                        new SelectListItem() {Text = "High", Value = "3"}
                    }
                };
                return PartialView("_EditTask", viewModel);
            }
        }

        private TaskEditViewModel getDefaultAsideViewModel()
        {
            TaskEditViewModel viewModel = new TaskEditViewModel
            {
                //Defaults
                Id = 0,
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
            return viewModel;
        }

        public ActionResult GetAsideDragTab()
        {
            return PartialView("");
        }
    }
}