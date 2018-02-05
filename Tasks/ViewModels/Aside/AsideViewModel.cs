using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Service.Aside.Dto;

namespace Tasks.ViewModels.Aside
{
    public class AsideViewModel
    {
        public List<Tab> VisibleTabsList { get; set; }
        public List<Tab> HiddenTabsList { get; set; }
        public AsideViewModel()
        {
            this.VisibleTabsList = new List<Tab>();
        }
    }
}