using System;
using Tasks.Infrastructure.Extension;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels.Thoughts.Entity
{
    public class Thought : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual int SortId { get; set; }
        public virtual int TimeFrameId { get; set; }
        public virtual DateTime TimeFrameDateTime { get; set; }
        public virtual Project Project { get; set; }

        public static Thought Create(string description, int sortId, int timeFrameId, DateTime timeFrameDateTime, Project project)
        {
            DateTime validatedTimeFrameDateTime;
            switch ((TimeFrameType) timeFrameId)
            {
                case TimeFrameType.Open:
                    validatedTimeFrameDateTime = new DateTime(2050, 1, 1);
                    break;
                case TimeFrameType.Date:
                    validatedTimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month,
                        timeFrameDateTime.Day);
                    break;
                case TimeFrameType.Time:
                    validatedTimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day, 
                                                              timeFrameDateTime.Hour, timeFrameDateTime.Minute, 0);
                    break;
                case TimeFrameType.Week:
                    //Make sure a Monday
                    validatedTimeFrameDateTime =
                        new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day)
                            .StartOfWeek(DayOfWeek.Monday);
                    break;
                case TimeFrameType.Month:
                    validatedTimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, 1);
                    break;
                default:
                    validatedTimeFrameDateTime = new DateTime(2050, 1, 1);
                    break;
            }

            Thought thought = new Thought()
            {
                User = new User().Create(),
                Description = description,
                CreatedDateTime = DateTime.Now,
                SortId = sortId,
                TimeFrameId = timeFrameId,
                TimeFrameDateTime = validatedTimeFrameDateTime,
                Project = project
            };
            return thought;
        }

        public virtual void Update(string description, int timeFrameId, DateTime timeFrameDateTime, Project project)
        {
            Description = description;
            Project = project;

            TimeFrameId = timeFrameId;
            switch ((TimeFrameType)timeFrameId)
            {
                case TimeFrameType.Open:
                    TimeFrameDateTime = new DateTime(2050,1,1);
                    break;
                case TimeFrameType.Date:
                    TimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day);
                    break;
                case TimeFrameType.Time:
                    TimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day, timeFrameDateTime.Hour, timeFrameDateTime.Minute,0);
                    break;
                case TimeFrameType.Week:
                    //Make sure a Monday
                    TimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day).StartOfWeek(DayOfWeek.Monday);
                    break;
                case TimeFrameType.Month:
                    TimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, 1);
                    break;
            }
        }
    }
}