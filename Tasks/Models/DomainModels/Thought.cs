using System;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class Thought : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual int SortId { get; set; }
    }
}