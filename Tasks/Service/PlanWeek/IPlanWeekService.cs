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
        Dictionary<DayOfWeek, ItemListDto> GetCurrentWeekItems();
        Dictionary<DayOfWeek, ItemListDto> GetWeekItems(DateTime weekCommencingDate);
        Dictionary<TimeFrameType, ItemListDto> GetCurrentOpenItems();
        Dictionary<TimeFrameType, ItemListDto> GetOpenItems(DateTime date);
        ItemListDto GetDayItems(DateTime date);
    }
}