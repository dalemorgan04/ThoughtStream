using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Service.CalendarEvents;
using Tasks.ViewModels.CalendarEvents;

namespace Tasks.Controllers
{
    public class CalendarEventsController : BaseController, IAsideController
    {
        private ICalendarEventService calendarEventService;
        public CalendarEventsController(ICalendarEventService calendarEventService)
        {
            this.calendarEventService = calendarEventService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAside()
        {
            return PartialView("_Aside");
        }
    }
}