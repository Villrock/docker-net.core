using System;
using QFlow.Models.DataModels.Alerts;
using QFlow.Models.DataModels.Requests;

namespace QFlow.Models.DataModels.Emails
{
    public class EmailNotification
    {
        public int Id { get; set; }

        /// <summary>
        /// Emails by comma
        /// </summary>
        public string EmailsTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsProcessing { get; set; }
        public DateTime? SendDate { get; set; }
        public bool IsSent { get; set; }

        public int? RequestId { get; set; }
        public Request Request { get; set; }

        public int? AlertTypeId { get; set; }
        public AlertType AlertType { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Completed { get; set; }
    }
}
