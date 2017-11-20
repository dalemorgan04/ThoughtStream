using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.DomainModels;

namespace Tasks.Service
{
    public class TaskTimeFrameDto
    {
        public int Id { get; set; }
        public TimeFrameType Type { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Due { get; set; }
    }
}