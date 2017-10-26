using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models;
using Tasks.Models.DomainModels;

namespace Tasks.Repository
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).Column("UserId").GeneratedBy.Native();
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
            
    }
}