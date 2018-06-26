using System;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters;
using Tasks.Infrastructure.Extension;
using Tasks.Models.DomainModels.Projects.Entity;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class Task : IDomainEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Description { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual int TimeFrameId { get; set; }
        public virtual DateTime TimeFrameDateTime { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual Project Project { get; set; }

        public static Task Create(string description, int timeFrameId, DateTime timeFrameDateTime, bool isComplete)
        {
            DateTime validatedTimeFrameDateTime;
            switch ((TimeFrameType)timeFrameId)
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

            Task task = new Task()
            {
                User = new User(),
                Description = description,
                Priority = Priority.Create(),
                TimeFrameId = timeFrameId,
                TimeFrameDateTime = timeFrameDateTime,
                IsComplete = isComplete
            };
            return task;
        }

        public virtual void Update(string description, 
                                   Priority priority, 
                                   int timeFrameId,
                                   DateTime timeFrameDateTime, 
                                   bool isComplete,
                                   Project project)
        {
            Description = description;
            Priority = priority;
            IsComplete = isComplete;
            Project = project;

            TimeFrameId = timeFrameId;
            switch ((TimeFrameType)timeFrameId)
            {
                case TimeFrameType.Open:
                    TimeFrameDateTime = new DateTime(2050, 1, 1);
                    break;
                case TimeFrameType.Date:
                    TimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day);
                    break;
                case TimeFrameType.Time:
                    TimeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day, timeFrameDateTime.Hour, timeFrameDateTime.Minute, 0);
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

        public virtual void Update(Project project)
        {
            this.Project = project;
        }
    }
}

