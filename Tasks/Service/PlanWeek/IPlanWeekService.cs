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
        InWeekItemListDto GetCurrentWeekItems();
        InWeekItemListDto GetWeekItems(DateTime weekCommencingDate);
        OpenItemListDto GetCurrentOpenItems();
        OpenItemListDto GetOpenItems(DateTime date);
        ItemListDto GetDayItems(DateTime date);
    }
}