using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Projects;
using Tasks.Service.Projects.Dto;
using Tasks.ViewModels.Projects;

namespace Tasks.Controllers
{
    public class ProjectsController : BaseController, IAsideController
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
            if (projectId == 0)
            {
                ProjectEditViewModel viewmodel = getDefaultViewModel();
                return PartialView("_EditProject", viewmodel);
            }
            else
            {
                //TODO get requested project in basic form
                ProjectEditViewModel viewmodel = new ProjectEditViewModel()
                {
                    Id = 0,
                    Description = ""
                };
                return PartialView("_EditProject", viewmodel);
            }
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
    }
}