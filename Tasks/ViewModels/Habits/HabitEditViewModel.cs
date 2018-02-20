using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Habits.Entity;

namespace Tasks.ViewModels.Habits
{
    public class HabitEditViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int PriorityId { get; set; }
        public List<SelectListItem> PriorityDropDownItems { get; set; }

        public TimeFrameType TimeFrameType { get; set; }
        
        public virtual int RepeatInterval { get; set; }
        public virtual int RepeatCount { get; set; }
        public virtual int DayOfWeek { get; set; }
        public virtual int DayOfMonth { get; set; }
        public virtual int WeekdayOfMonth { get; set; }
        public virtual int WeekOfYear { get; set; }
        public virtual int MonthNo { get; set; }
        public virtual DateTime StartOnDate { get; set; }
        public virtual DateTime EndOnDate { get; set; }
        public virtual int EndOnOccurrenceNo { get; set; }

    }
}