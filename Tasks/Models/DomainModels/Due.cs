using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Tasks.Models.Core;

namespace Tasks.Models.DomainModels
{
    public class Due : IDomainEntity<int>
    {

        public virtual int Id { get; set; }
        public virtual DateTime DateTime { get; set; }

        public override string ToString()
        {
            switch ((TimeFrameType)Id)
            {
                case TimeFrameType.Time:
                    return DateTime.ToShortTimeString();
                case TimeFrameType.Date:
                    return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day).ToShortDateString();
                case TimeFrameType.Week:
                    string from = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day).ToShortDateString();
                    string to = new DateTime(DateTime.AddDays(7).Year, DateTime.AddDays(7).Month, DateTime.AddDays(7).Day).ToShortDateString();
                    return string.Format("{0} - {1}",from,to);
                case TimeFrameType.Month:
                    return new DateTime(DateTime.Year, DateTime.Month, 1);
                case TimeFrameType.Year:
                    return new DateTime(DateTime.Year, 1, 1);
                default:
                    return DateTime.MaxValue;            
            }
        }
        
        public WeekCommencing WeekCommencing
        {
            get => new WeekCommencing(DateTime);
            set => DateTime = new DateTime(value.CommenceDate.Year, value.CommenceDate.Month, value.CommenceDate.Day);
        }
        
    }
}

public class WeekCommencing
{
    public DateTime CommenceDate { get;}
    public WeekCommencing(DateTime commenceDate)
    {
        this.CommenceDate = commenceDate;
    }
    public DateTime EndDate => CommenceDate.AddDays(7);
    public override string ToString()
    {
        return string.Format("{0} - {1}", CommenceDate.ToShortDateString() + " - " + EndDate.ToShortDateString());
    }
}

