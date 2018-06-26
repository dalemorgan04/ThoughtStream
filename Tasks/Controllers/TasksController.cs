using System;
using System.Collections.Generic;
using System.Globalization;
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
                TaskList = new List<TaskViewModel>()
            };
            foreach (var task in taskList)
            {
                viewModel.TaskList.Add( new TaskViewModel()
                {
                    Id = task.Id,
                    Description = task.Description,
                    PriorityId = task.Priority.Id,
                    IsComplete = task.IsComplete,
                    TimeFrameId = (int)task.TimeFrame.TimeFrameType,
                    TimeFrameDateString = task.TimeFrame.DateString,
                    TimeFrameTimeString = task.TimeFrame.TimeString,
                    TimeFrameDueString = task.TimeFrame.DueString
                });
            }
            return View("Index",viewModel);
        }

        [HttpPost]
        public ActionResult GetAddTask()
        {
            TaskViewModel viewModel = new TaskViewModel();
            return PartialView("_AddTask", viewModel);
        }

        [HttpPost]
        public ActionResult GetTasksTable()
        {
            TasksViewModel viewModel = new TasksViewModel();
            List<TaskDto> taskList = taskService.GetTasks().ToList();
            return PartialView("_TasksTable", viewModel);
        }

        [HttpPost]
        public bool Create(TaskViewModel viewModel)
        {

            DateTime dateTime;
            DateTime.TryParseExact(
                $"{viewModel.TimeFrameDateString} {viewModel.TimeFrameTimeString}",
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out dateTime);

            TaskDto task = new TaskDto()
            {
                Description = viewModel.Description,
                TimeFrame = new TimeFrame((TimeFrameType)viewModel.TimeFrameId, dateTime)
            };
            taskService.Save(task);
            return true;
        }

        public object Update(TaskViewModel viewModel)
        {
            DateTime date = DateTime.ParseExact(viewModel.TimeFrameDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateTime;
            if ((TimeFrameType)viewModel.TimeFrameId == TimeFrameType.Time)
            {
                TimeSpan time = TimeSpan.ParseExact(viewModel.TimeFrameTimeString, "hh\\:mm", CultureInfo.InvariantCulture);
                dateTime = date.Add(time);
            }
            else
            {
                dateTime = date;
            }

            TaskDto thoughtDto = new TaskDto()
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                TimeFrame = new TimeFrame((TimeFrameType)viewModel.TimeFrameId, dateTime)
            };
            taskService.Save(thoughtDto);
            return true;
        }
        
        
        //Aside

        public ActionResult GetAside()
        {
            return PartialView("_Aside");
        }

        public ActionResult GetAsideAddTab()
        {
            TaskViewModel viewModel = getDefaultAsideViewModel();
            return PartialView("_AddTask", viewModel);
        }

        public ActionResult GetAsideEditSelectTab(int taskId)
        {
            TaskDto task = taskService.GetTaskById(taskId);
            TaskViewModel viewModel = new TaskViewModel()
            {
                Id = task.Id,
                Description = task.Description,
                PriorityId = task.Priority.Id,
                TimeFrameId = (int)task.TimeFrame.TimeFrameType,
                TimeFrameDateString = task.TimeFrame.DateString,
                TimeFrameTimeString = task.TimeFrame.TimeString,
                TimeFrameDueString = task.TimeFrame.DueString
            };
            return PartialView("_EditTaskSelect", viewModel);
        }

        private TaskViewModel getDefaultAsideViewModel()
        {
            TaskViewModel viewModel = new TaskViewModel
            {
                Id = 0,
                Description = "",
                PriorityId = 1,
                IsComplete = false,
                TimeFrameId = (int)TimeFrameType.Open,
                TimeFrameDateString = "01/01/2050",
                TimeFrameTimeString = "00:00"
            };
            return viewModel;
        }

        private string validateViewModel(TaskViewModel viewModel)
        {
            return "";
        }
    }
}