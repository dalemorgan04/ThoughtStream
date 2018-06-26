using FluentNHibernate.Mapping;
using Tasks.Models.DomainModels;

namespace Tasks.Repository.Tasks
{
    public class TaskMap: ClassMap<Task>
    {
        public TaskMap()
        {
            Table("Tasks");
            Id(x => x.Id).Column("TaskId").GeneratedBy.Native();
            References(x => x.User).Column("UserId");                
            Map(x => x.Description);
            References(x => x.Priority).Column("PriorityId");
            Map(x => x.IsComplete);
            Map(x => x.TimeFrameId);
            Map( x => x.TimeFrameDateTime);
            References(x => x.Project).Column("ProjectId");
            //Map(x => x.IsComplete).Column("IsComplete");
        }
    }
}