using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels;

namespace Tasks.Repository.Thoughts
{
    public class ThoughtMap : ClassMap<Thought>
    {
        public ThoughtMap()
        {
            Table("Thoughts");
            Id(x => x.Id).Column("ThoughtId").GeneratedBy.Native();
            References(x => x.User).Column("UserId");
            Map(x => x.Description);
            Map(x => x.DateCreated);
            Map(x => x.SortId);
        }    
    }
}