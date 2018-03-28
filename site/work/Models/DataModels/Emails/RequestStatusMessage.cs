using QFlow.Models.DataModels.Requests;

namespace QFlow.Models.DataModels.Emails
{
    public class RequestStatusMessage
    {
        public int Id { get; set; }

        public int RequestStatusId { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
