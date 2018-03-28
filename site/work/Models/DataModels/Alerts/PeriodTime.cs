using System;

namespace QFlow.Models.DataModels.Alerts
{
    public class PeriodTime
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime GetDateTimeFrom(DateTime startDateTime)
        {
            switch(Name)
            {
                case "1 day": return startDateTime.AddDays(-1);
                case "2 days": return startDateTime.AddDays(-2);
                case "3 days": return startDateTime.AddDays(-3);
                case "4 days": return startDateTime.AddDays(-4);
                case "1 week": return startDateTime.AddDays(-7);
                default: return startDateTime;
            }
        }
    }
}
