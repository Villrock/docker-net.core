namespace QFlow.Models.DataModels.Alerts
{
    public class AlertTypePeriodTime
    {
        public int Id { get; set; }

        public int AlertTypeId { get; set; }
        public AlertType AlertType { get; set; }

        public int PeriodTimeId { get; set; }
        public PeriodTime PeriodTime { get; set; }
    }
}
