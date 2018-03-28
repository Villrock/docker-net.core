using System;
using QFlow.Models.DataModels.Requests;

namespace QFlow.Models.DataModels.Alerts
{
    public class AlertNotification
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public int? RequestId { get; set; }
        public Request Request { get; set; }

        public int? AlertTypeId { get; set; }
        public AlertType AlertType { get; set; }

        public string Email { get; set; }
    }
}
