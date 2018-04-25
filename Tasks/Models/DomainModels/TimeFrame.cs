using System;
using System.Globalization;
using Microsoft.Ajax.Utilities;
using Tasks.Infrastructure.Extension;

namespace Tasks.Models.DomainModels
{
    public class TimeFrame
    {
        private readonly TimeFrameType timeFrameType;
        private readonly DateTime timeFrameDateTime;

        public TimeFrameType TimeFrameType => timeFrameType;
        public DateTime TimeFrameDateTime => timeFrameDateTime;
        public String DateString => timeFrameDateTime.ToString("dd/MM/yyyy");
        public string TimeString => timeFrameDateTime.ToString("HH:mm");
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
            switch (timeFrameType)
            {
                case TimeFrameType.Date:
                    this.timeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day);
                    break;
                case TimeFrameType.Time:
                    this.timeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day,
                                                          timeFrameDateTime.Hour, timeFrameDateTime.Minute, 0);
                    break;
                case TimeFrameType.Week:
                    this.timeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, timeFrameDateTime.Day)
                                                          .StartOfWeek(DayOfWeek.Monday);
                    break;
                case TimeFrameType.Month:
                    this.timeFrameDateTime = new DateTime(timeFrameDateTime.Year, timeFrameDateTime.Month, 1);
                    break;
                default:
                    this.timeFrameDateTime = new DateTime(2050, 1, 1);
                    break;
            }
        }

        private string getDueString()
        {
            switch (TimeFrameType)
            {
                case TimeFrameType.Open:
                    return "";
                case TimeFrameType.Time:
                    string timeString = TimeFrameDateTime.ToString("h:mmtt").ToLower();
                    return $"{getDateString()} at {timeString}";
                case TimeFrameType.Date:
                    return getDateString();
                case TimeFrameType.Week:
                    return getWeekString();
                case TimeFrameType.Month:
                    return TimeFrameDateTime.ToString("MMMM yy");
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
                DateTime to = new DateTime(timeFrameDateTime.AddDays(7).Year, timeFrameDateTime.AddDays(7).Month,
                    timeFrameDateTime.AddDays(7).Day);
                var cal = DateTimeFormatInfo.CurrentInfo.Calendar;
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

        private string getDateString()
        { 
            int daysAway = Math.Abs((TimeFrameDateTime - DateTime.Now).Days);
            bool inPast = TimeFrameDateTime < DateTime.Now;
            //If less than a week state the day name
            switch (daysAway)
            {
                case 0:
                    return $"Today";
                case 1:
                    if (inPast)
                    {
                        return $"Yesterday";
                    }
                    else
                    {
                        return $"Tomorrow";
                    }
                default:
                    if (daysAway <= 7)
                    {
                        string weekday = TimeFrameDateTime.ToString("dddd");
                        if (inPast)
                        {
                            return $"Last {weekday}";
                        }
                        else
                        {
                            return weekday;
                        }
                    }
                    else
                    {
                        return TimeFrameDateTime.ToString("d/M/yy");
                    }
            }
        }
    }
}