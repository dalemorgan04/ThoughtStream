using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Infrastructure.Extension;
using Tasks.Models.DomainModels;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Habits;
using Tasks.Service.Habits.Dto;
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

        public ActionResult Index()
        {
            HabitsViewModel viewModel = new HabitsViewModel() { HabitList = habitService.GetHabits() };
            return View("Index", viewModel);
        }
        public ActionResult GetAside()
        {
            return PartialView("_Aside");
        }

        public ActionResult GetAsideAddTab()
        {
            HabitEditViewModel viewmodel = getDefaultHabit();
            return PartialView("_AddHabit", viewmodel);
        }

        public ActionResult GetAsideEditTab(int habitId)
        {
            if (habitId == 0)
            {
                HabitEditViewModel viewmodel = getDefaultHabit();
                return PartialView("_EditHabit", viewmodel);
            }
            else
            {
                HabitEditViewModel viewModel = new HabitEditViewModel()
                {
                    Id = 0,
                    Description = "",
                    PriorityId = 1,
                    TimeFrameType = TimeFrameType.Date,
                    RepeatInterval = 0,
                    RepeatCount = 0,
                    DayOfWeek = (int) DateTime.Now.DayOfWeek, // 0 = Sunday
                    DayOfMonth = DateTime.Now.Day,
                    WeekdayOfMonth = (int) DateTime.Now.DayOfWeek,
                    WeekOfYear = DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(DateTime.Now,
                        new CultureInfo("en-US").DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday),
                    MonthNo = DateTime.Now.Month,
                    StartOnDate = DateTime.Now,
                    EndOnOccurrenceNo = 0,

                    PriorityDropDownItems = new List<SelectListItem>()
                    {
                        new SelectListItem(){Text = "Low", Value = "1" , Selected = true},
                        new SelectListItem(){Text = "Medium", Value = "2"},
                        new SelectListItem(){Text = "High", Value = "3"}
                    }
                };
                return PartialView("_EditHabit", viewModel);
            }
            
        }

        private HabitEditViewModel getDefaultHabit()
        {
            HabitEditViewModel viewmodel = new HabitEditViewModel()
            {
                Id = 0,
                Description = "",
                PriorityId = 1,
                TimeFrameType = TimeFrameType.Date,
                RepeatInterval = 0,
                RepeatCount = 0,
                DayOfWeek = (int)DateTime.Now.DayOfWeek, // 0 = Sunday
                DayOfMonth = DateTime.Now.Day,
                WeekdayOfMonth = (int)DateTime.Now.DayOfWeek,
                WeekOfYear = DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(DateTime.Now,
                    new CultureInfo("en-US").DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday),
                MonthNo = DateTime.Now.Month,
                StartOnDate = DateTime.Now,
                EndOnOccurrenceNo = 0,

                PriorityDropDownItems = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Low", Value = "1" , Selected = true},
                    new SelectListItem(){Text = "Medium", Value = "2"},
                    new SelectListItem(){Text = "High", Value = "3"}
                }
            };
            return viewmodel;
        }
    }
}