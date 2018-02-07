using System;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class TimeFrame
    {
        private readonly int id;
        private readonly DateTime? nullableDateTime;
        public TimeFrame(int TimeFrameId, DateTime? dateTime = null)
        {
            this.id = TimeFrameId;
            this.nullableDateTime = dateTime;
        }

        public int TimeFrameId { get { return id; } }                
        public virtual string Description
        {
            get
            {
                switch ((TimeFrameType)this.id)
                {
                    case TimeFrameType.Time:
                        return "Time";
                    case TimeFrameType.Date:
                        return "Day";
                    case TimeFrameType.Week:
                        return "Week";
                    case TimeFrameType.Month:
                        return "Month";
                    default:
                        return "Error";
                };
            }         
        }

        public virtual string Due 
        {
            get
            {
                if (nullableDateTime is null)
                {
                    return "";
                }
                else
                {
                    DateTime dateTime = (DateTime)nullableDateTime;
                    switch ((TimeFrameType)this.id)
                    {
                        case TimeFrameType.Time:
                            string time = dateTime.ToShortTimeString();
                            string day;
                            if (dateTime < DateTime.Now.AddDays(7))
                            {
                                day = dateTime.ToString("ddd");
                            }
                            else
                            {
                                day = dateTime.ToShortDateString();
                            }
                            return dateTime.ToShortTimeString();
                        case TimeFrameType.Date:
                            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToShortDateString();
                        case TimeFrameType.Week:
                            string from = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToShortDateString();
                            string to = new DateTime(dateTime.AddDays(7).Year, dateTime.AddDays(7).Month, dateTime.AddDays(7).Day).ToShortDateString();
                            return string.Format("{0} - {1}", from, to);
                        case TimeFrameType.Month:
                            return string.Format("{mmm}", new DateTime(dateTime.Year, dateTime.Month, 1));
                        default:
                            return "Someday";
                    }
                };
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}