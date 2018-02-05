using System.Collections.Generic;
using Tasks.Models.DomainModels;

namespace Tasks.Service.PlanWeek.Dto
{
    public class OpenItemListDto
    {
        public Dictionary<TimeFrameType, ItemListDto> openItems { get; private set; }

        public OpenItemListDto()
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