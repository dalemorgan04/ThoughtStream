using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models;
using Tasks.Models.DomainModels;

namespace Tasks.Repository
{
    public class PriorityMap: ClassMap<Priority>
    {
        public PriorityMap()
        {
            Table("Priority");
            Id(x => x.Id).Column("PriorityId").GeneratedBy.Identity();
            Map(x => x.Description);               
        }
    }
}