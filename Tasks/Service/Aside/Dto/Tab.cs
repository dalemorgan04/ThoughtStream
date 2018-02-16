using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels.Enum;

namespace Tasks.Service.Aside.Dto
{
    public class Tab
    {   
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDefault { get; set; }
        public int OrderNumber { get; set; }
    }
}