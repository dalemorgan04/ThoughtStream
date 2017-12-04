using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Service.PlanWeek;
using Tasks.Service.PlanWeek.Dto;
using Tasks.ViewModels.PlanWeek;

namespace Tasks.Controllers
{
    public class PlanWeekController : Controller
    {
        IPlanWeekService planWeekService;

        public PlanWeekController(
            IPlanWeekService planWeekService)
        {
            this.planWeekService = planWeekService;
        }        

        public ActionResult Index()
        {
            WeekPlanDto weekPlanDto = planWeekService.GetCurrentWeekPlan();
            PlanWeekViewModel viewModel = new PlanWeekViewModel()
            {
                weekPlanDto = weekPlanDto
            };
            return View("Index", viewModel);
        }
    }
}