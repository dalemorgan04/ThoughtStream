using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class User : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual User Create()
        {
            User user = new User()
            {
                Id = 1,
                FirstName = "Dale",
                LastName = "Morgan"
            };
            return user;
        }
    }

}   