using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels.Enum;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Projects;
using Tasks.Service.Projects.Dto;
using Tasks.ViewModels.Aside;
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

        public ActionResult GetDefaultAsideLayout()
        {
            var viewModel = new AsideViewModel()
            {
                VisibleTabsList = new List<Tab>()
                {
                    {new Tab(){ OrderNumber = 0, TabType = AsideTabType.Select, Name = "Selection"} },
                    {new Tab(){ OrderNumber = 2, TabType = AsideTabType.Thoughts, Name = "Thoughts"} }
                }
            };
            return PartialView("_Aside", viewModel);
        }

        public ActionResult GetDefaultAsideContent()
        {
            throw new NotImplementedException();
        }
    }
}