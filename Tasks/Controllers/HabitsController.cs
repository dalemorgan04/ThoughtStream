using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Service.Habits;
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

        public ActionResult Aside()
        {
            return PartialView("_Aside");
        }

        public ActionResult Index()
        {
            HabitsViewModel viewModel = new HabitsViewModel() { HabitList = habitService.GetHabits() };
            return View("Index", viewModel);
        }
    }
}