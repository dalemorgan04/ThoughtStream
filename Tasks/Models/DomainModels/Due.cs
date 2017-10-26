using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models.Core;

namespace Tasks.Models.DomainModels
{
    public class Due : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        //TODO public virtual string Description { get; set; }
        public virtual TimeSpan Time { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime WeekCommencing { get; set; }
        public virtual int Month { get; set; }
        public virtual  int Year { get; set; }
 
    }
}