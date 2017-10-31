using System;

namespace Tasks.Models.DomainModels
{
    public class Timeframe
    {
        public virtual int TimeFrameId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public string Window
        {
            get
            {
                switch ((TimeFrameType)TimeFrameId)
                {
                    case TimeFrameType.Time:
                        return "Time";
                    case TimeFrameType.Date:
                        return "Day";
                    case TimeFrameType.Week:
                        return "Week";
                    case TimeFrameType.Month:
                        return "Month";
                    case TimeFrameType.Year:
                        return "Year";
                    default:
                        return "Error";
                }
            }
        }

        public string Due
        {
            get
            {
                switch ((TimeFrameType)TimeFrameId)
                {
                    case TimeFrameType.Time:
                        string time = DateTime.ToShortTimeString();
                        string day;
                        if (DateTime < DateTime.AddDays(7))
                        {
                            day = DateTime.ToString("ddd");
                        }
                        else
                        {
                            day = DateTime.ToShortDateString();
                        }
                        return DateTime.ToShortTimeString();
                    case TimeFrameType.Date:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day).ToShortDateString();
                    case TimeFrameType.Week:
                        string from = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day).ToShortDateString();
                        string to = new DateTime(DateTime.AddDays(7).Year, DateTime.AddDays(7).Month, DateTime.AddDays(7).Day).ToShortDateString();
                        return string.Format("{0} - {1}", from, to);
                    case TimeFrameType.Month:
                        return string.Format("{mmm}", new DateTime(DateTime.Year, DateTime.Month, 1));
                    case TimeFrameType.Year:
                        return DateTime.Year.ToString();
                    default:
                        return "Someday";
                }
            }
        }

        public override string ToString()
        {
            return Due;
        }

    }
}