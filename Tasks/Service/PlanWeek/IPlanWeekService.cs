using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;
using Tasks.Service.PlanWeek.Dto;

namespace Tasks.Service.PlanWeek
{
    public interface IPlanWeekService
    {
        InWeekItemList GetCurrentWeekItems();
        InWeekItemList GetWeekItems(DateTime weekCommencingDate);
        OpenItemList GetCurrentOpenItems();
        OpenItemList GetOpenItems(DateTime date);
        ItemListDto GetDayItems(DateTime date);
    }
}