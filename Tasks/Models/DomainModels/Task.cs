using System;
using Tasks.Models.Core;

namespace Tasks.Models.DomainModels
{
    public class Task : IDomainEntity<int>
    {        
        public virtual int Id { get; set; }        
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Timeframe Timeframe { get; set; }
    }
}

