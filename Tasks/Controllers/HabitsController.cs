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
            throw new NotImplementedException();
        }

        public ActionResult Index()
        {
            HabitsViewModel viewModel = new HabitsViewModel() { HabitList = habitService.GetHabits() };
            return View("Index", viewModel);
        }
    }
}