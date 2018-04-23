using System;
using System.Globalization;
using System.Threading;
using Humanizer;
using Humanizer.DateTimeHumanizeStrategy;
using Tasks.Repository.Core;

namespace Tasks.Models.DomainModels
{
    public class TimeFrame
    {
        private readonly TimeFrameType timeFrameType;
        private readonly DateTime timeFrameDateTime;

        public TimeFrameType TimeFrameType => timeFrameType;
        public DateTime TimeFrameDateTime => timeFrameDateTime;
        public DateTime Date => timeFrameDateTime.Date;
        public TimeSpan Time => timeFrameDateTime.TimeOfDay;
        public string WeekString => getWeekString();
        public String DueString => getDueString();


        public TimeFrame()
        {
            this.timeFrameType = TimeFrameType.Open;
            this.timeFrameDateTime = new DateTime(2050,1,1); //Set date due to far off into the future
        }

        public TimeFrame(TimeFrameType timeFrameType, DateTime timeFrameDateTime)
        {
            this.timeFrameType = timeFrameType;
            this.timeFrameDateTime = timeFrameDateTime;
        }

        private string getDueString()
        {
            switch (TimeFrameType)
            {
                case TimeFrameType.Open:
                    return "";
                case TimeFrameType.Time:
                    return TimeFrameDateTime.Humanize();
                case TimeFrameType.Date:
                    return Date.Humanize();
                case TimeFrameType.Week:
                    return getWeekString();
                case TimeFrameType.Month:
                    return Date.ToString("MMMM yy");
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
                
                DateTime from = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day);
                DateTime to = new DateTime(timeFrameDateTime.AddDays(7).Year, timeFrameDateTime.AddDays(7).Month, timeFrameDateTime.AddDays(7).Day);
                var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                int weekNo = cal.GetWeekOfYear(from, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                string fromString;
                string toString = to.ToString("d/M/yy");
                if (from.Year == to.Year)
                {
                    if (from.Month == to.Month)
                    {
                        fromString = from.ToString("d");
                    }
                    else
                    {
                        fromString = from.ToString("d/M");
                    }
                }
                else
                {
                    fromString = from.ToString("d/M/yy");
                }
                return $"W{weekNo} ({fromString} - {toString})";
            }
        }

    }
}