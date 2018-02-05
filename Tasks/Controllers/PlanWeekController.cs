using System.Web.Mvc;
using Tasks.Service.PlanWeek;
using Tasks.ViewModels.PlanWeek;

namespace Tasks.Controllers
{
    public class PlanWeekController : BaseController
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
    }
}