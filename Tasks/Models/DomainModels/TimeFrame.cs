namespace Tasks.Models.DomainModels
{
    public class TimeFrame
    {
        public TimeFrameType TimeFrameId { get; set; }
        public override string ToString()
        {
            switch (TimeFrameId)
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
}