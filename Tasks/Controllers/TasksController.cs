using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Service.Tasks;
using Tasks.Service.Tasks.Dto;

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
            List<TaskDto> taskList = new List<TaskDto>();
            return View();
        }
    }
}