﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Service.Aside.Dto;

namespace Tasks.ViewModels.Aside
{
    public class AsideViewModel
    {
        public List<Tab> TabsList { get; set; }
        public AsideViewModel()
        {
            this.TabsList = new List<Tab>();
        }
    }
}