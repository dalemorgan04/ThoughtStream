using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;
using Tasks.Service.PlanWeek.Dto;
using Tasks.Service.Projects.Dto;

namespace Tasks.ViewModels.PlanWeek
{
    public class PlanWeekViewModel
    {
        public InWeekItemListDto InWeekItemsListDto { get; set; }
        public OpenItemListDto OpenItemsListDto { get; set; }
    }
}