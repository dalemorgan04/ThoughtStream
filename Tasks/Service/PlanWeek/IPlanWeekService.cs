using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Service.PlanWeek.Dto;

namespace Tasks.Service.PlanWeek
{
    public interface IPlanWeekService
    {
        WeekPlanDto GetCurrentWeekPlan();
    }
}