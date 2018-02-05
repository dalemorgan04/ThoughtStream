using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Enum;

namespace Tasks.Service.Aside.Dto
{
    public class Tab
    {
        public int OrderNumber { get; set; }
        public AsideTabType TabType { get; set; }
        public string Name { get; set; }
    }
}