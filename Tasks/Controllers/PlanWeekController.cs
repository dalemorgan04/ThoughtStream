using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Enum;
using Tasks.Service.Aside.Dto;
using Tasks.Service.PlanWeek;
using Tasks.ViewModels.Aside;
using Tasks.ViewModels.PlanWeek;
using Tasks.ViewModels.Tasks;

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
            return new EmptyResult();
        }
    }
}