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
        public InWeekItemList weekItemLists { get; set; }
        public OpenItemList openItemLists { get; set; }
    }
}