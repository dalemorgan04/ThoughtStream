using System;
using Humanizer;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class TimeFrame
    {
        private readonly TimeFrameType timeFrameType;
        private readonly DateTime dateTime;

        public TimeFrameType TimeFrameType => timeFrameType;
        public DateTime DateTime => dateTime;
        public DateTime Date => dateTime.Date;
        public TimeSpan Time => dateTime.TimeOfDay;
        public string WeekString => getWeekString();
        public String DueString => getDueString();


        public TimeFrame(TimeFrameType timeFrameType)
        {
            if (timeFrameType != TimeFrameType.Open)
            {
                throw new System.ArgumentNullException("Missing DateTime when creating TimeFrame object");
            }
            this.timeFrameType = timeFrameType;
            this.dateTime = new DateTime(2050,1,1); //Set date due to far off into the future
        }

        public TimeFrame(TimeFrameType timeFrameType, DateTime dateTime)
        {
            this.timeFrameType = timeFrameType;
            this.dateTime = dateTime;
        }

        private string getDueString()
        {
            switch (TimeFrameType)
            {
                case TimeFrameType.Time:
                    return DateTime.Humanize();
                case TimeFrameType.Date:
                    return Date.Humanize();
                case TimeFrameType.Week:
                    return getWeekString();
                case TimeFrameType.Month:
                    return Date.Month.ToString("MMMM");
                case TimeFrameType.Open:
                    return "";
                default:
                    return "";
            }
        }
        private string getWeekString()
        {
            if (timeFrameType != TimeFrameType.Week)
            {
                return "";
            }
            else
            {
                DateTime from = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                DateTime to = new DateTime(dateTime.AddDays(7).Year, dateTime.AddDays(7).Month, dateTime.AddDays(7).Day);
                string fromString;
                string toString = to.ToShortDateString();
                if (from.Year == to.Year)
                {
                    if (from.Month == to.Month)
                    {
                        fromString = from.Day.ToString();
                    }
                    else
                    {
                        fromString = $"{from.Day}/{from.Month}";
                    }
                }
                else
                {
                    fromString = from.ToShortDateString();
                }
                return $"Week {fromString} - {toString}";
            }
        }
    }
}