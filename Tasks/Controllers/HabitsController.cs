using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Service.Habits;
using Tasks.ViewModels.Habits;

namespace Tasks.Controllers
{
    public class HabitsController : Controller
    {
        private readonly IHabitService habitService;

        public HabitsController(
            IHabitService habitService)
        {
            this.habitService = habitService;
        }
        public ActionResult Index()
        {
            habitService.GetHabitOccurrencesOnDate(DateTime.Now);
            return View();
        }
    }
}