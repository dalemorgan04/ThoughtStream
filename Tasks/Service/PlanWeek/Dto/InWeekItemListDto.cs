using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Tasks.Service.PlanWeek.Dto
{
    public class InWeekItemListDto
    {
        public Dictionary<DayOfWeek, ItemListDto> dayItems { get; set; }

        public InWeekItemListDto()
        {
            dayItems = new Dictionary<DayOfWeek, ItemListDto>
            {
                { DayOfWeek.Monday, new ItemListDto() },
                { DayOfWeek.Tuesday, new ItemListDto() },
                { DayOfWeek.Wednesday, new ItemListDto() },
                { DayOfWeek.Thursday, new ItemListDto() },
                { DayOfWeek.Friday, new ItemListDto() },
                { DayOfWeek.Saturday, new ItemListDto() },
                { DayOfWeek.Sunday, new ItemListDto() }
            };
        }

        public void Update(DayOfWeek dayOfWeek, ItemListDto newItemListDto)
        {
            this.dayItems[dayOfWeek] = newItemListDto;
        }
    }
}