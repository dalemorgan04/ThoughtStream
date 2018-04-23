using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Thoughts;
using Tasks.Models.DomainModels.Thoughts.Entity;

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
            Map(x => x.CreatedDateTime);
            Map(x => x.SortId);
            Map(x => x.TimeFrameId);
            Map(x => x.TimeFrameDateTime);
            References(x => x.Project).Column("ProjectId");
        }    
    }
}