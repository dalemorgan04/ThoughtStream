using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Service.Projects;
using Tasks.Service.Projects.Dto;
using Tasks.ViewModels.Projects;

namespace Tasks.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService projectService;
        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public ActionResult Index()
        {            
            ProjectsViewModel viewModel = new ProjectsViewModel();
            viewModel.Projects = projectService.GetRootProjects();
            return View("Index",viewModel);
        }
    }
}