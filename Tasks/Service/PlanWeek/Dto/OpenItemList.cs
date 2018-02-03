using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;

namespace Tasks.Service.PlanWeek.Dto
{
    public class OpenItemList
    {
        public Dictionary<TimeFrameType, ItemListDto> openItems { get; set; }

        public OpenItemList()
        {
            this.openItems = new Dictionary<TimeFrameType, ItemListDto>()
            {
                {TimeFrameType.Week, new ItemListDto()},
                {TimeFrameType.Month, new ItemListDto()},
                {TimeFrameType.Year, new ItemListDto()},
                {TimeFrameType.Open, new ItemListDto()}
            };
        }

        public void Update(TimeFrameType timeFrame, ItemListDto newListDto)
        {
            this.openItems[timeFrame] = newListDto;
        }
    }
}