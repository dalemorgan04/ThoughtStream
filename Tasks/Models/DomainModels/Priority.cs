using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class Priority : IDomainEntity<int>
    {
        public virtual int Id { get; set; }        
        public virtual string Description { get; set; }        
    }
}