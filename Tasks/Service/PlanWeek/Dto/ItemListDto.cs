using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasks.Service.PlanWeek.Dto
{
    public class ItemListDto
    {
        public List<ItemDto> ItemDtos { get; set; }
        public ItemListDto()
        {
            this.ItemDtos = new List<ItemDto>();
        }
    }
}