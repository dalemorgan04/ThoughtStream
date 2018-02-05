using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels.Enum;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Habits;
using Tasks.ViewModels.Aside;
using Tasks.ViewModels.Habits;

namespace Tasks.Controllers
{
    public class HabitsController : BaseController, IAsideController
    {
        private readonly IHabitService habitService;        

        public HabitsController(
            IHabitService habitService)
        {
            this.habitService = habitService;
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

        public ActionResult Index()
        {
            HabitsViewModel viewModel = new HabitsViewModel() { HabitList = habitService.GetHabits() };
            return View("Index", viewModel);
        }
    }
}