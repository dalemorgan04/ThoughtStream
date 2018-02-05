using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels.Enum;
using Tasks.Service.Aside.Dto;
using Tasks.Service.PlanWeek;
using Tasks.ViewModels.Aside;
using Tasks.ViewModels.PlanWeek;

namespace Tasks.Controllers
{
    public class PlanWeekController : BaseController, IAsideController
    {
        readonly IPlanWeekService planWeekService;

        public PlanWeekController(
            IPlanWeekService planWeekService)
        {
            this.planWeekService = planWeekService;
        }        

        public ActionResult Index()
        {
            PlanWeekViewModel viewModel = new PlanWeekViewModel()
            {
                //List of tasks per day in current week
                InWeekItemsListDto = planWeekService.GetCurrentWeekItems(),
                //Open ended tasks i.e. week, month, year
                OpenItemsListDto = planWeekService.GetCurrentOpenItems()
                
            };
            
            return View("Index", viewModel);
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
            throw new System.NotImplementedException();
        }
    }
}