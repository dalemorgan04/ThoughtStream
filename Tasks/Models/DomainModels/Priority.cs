using NHibernate.Linq;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class Priority : IDomainEntity<int>
    {
        public virtual int Id { get; set; }        
        public virtual string Description { get; set; }

        public static Priority Create()
        {
            Priority priority = new Priority()
            {
                Id = 1,
                Description = "Test"
            };
            return priority;
        }
    }
}