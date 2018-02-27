using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Projects;
using Tasks.Service.Projects.Dto;
using Tasks.Service.Tasks;
using Tasks.Service.Tasks.Dto;
using Tasks.ViewModels.Projects;
using Tasks.ViewModels.Tasks;

namespace Tasks.Controllers
{
    public class ProjectsController : BaseController, IAsideController
    {
        private readonly IProjectService projectService;
        private readonly ITaskService taskService;
        public ProjectsController(
            IProjectService projectService,
            ITaskService taskService)
        {
            this.projectService = projectService;
            this.taskService = taskService;
        }

        public ActionResult Index()
        {            
            ProjectsViewModel viewModel = new ProjectsViewModel();
            viewModel.Projects = projectService.GetRootProjects();
            return View("Index",viewModel);
        }

        public ActionResult GetProjectsTable()
        {
            ProjectsViewModel viewModel = new ProjectsViewModel
            {
                Projects = projectService.GetRootProjects()
            };
            return PartialView("_ProjectsTable", viewModel);
        }

        public ActionResult GetAside()
        {
            return PartialView("_Aside");
        }

        public ActionResult GetAsideAddTab()
        {
            ProjectEditViewModel viewmodel = getDefaultViewModel();
            return PartialView("_AddProject", viewmodel);
        }

        public ActionResult GetAsideEditTab( int projectId)
        {
            
            ProjectWithItemsDto projectWithItems = projectService.GetProject(projectId);
            ProjectEditViewModel viewmodel = new ProjectEditViewModel()
            {
                Id = projectWithItems.Id,
                Description = projectWithItems.Description
            };
            return PartialView("_EditProject", viewmodel);
        }

        public ActionResult GetAsideAddTask()
        {
            TasksViewModel viewmodel = new TasksViewModel();
            List<TaskDto> taskList = taskService.GetTasks().ToList();
            viewmodel.TaskList = taskList;

            return PartialView("_AddTask", viewmodel);
        }

        private ProjectEditViewModel getDefaultViewModel()
        {
            ProjectEditViewModel viewmodel = new ProjectEditViewModel()
            {
                Id = 0,
                Description = ""
            };
            return viewmodel;
        }

        public bool Create(ProjectEditViewModel viewmodel)
        {
            ProjectWithItemsDto projectWithItemsDto = new ProjectWithItemsDto()
            {
                 
            };

            projectService.Save(projectWithItemsDto);
            return true;
        }

        public bool Update()
        {
            return true;
        }

        public bool AppendTask(int projectId, int taskId)
        {
            projectService.AppendTask(projectId, taskId);
            return true;
        }

        public bool AppendHabit()
        {
            return true;
        }

        public bool AppendProject()
        {
            return true;
        }
    }
}