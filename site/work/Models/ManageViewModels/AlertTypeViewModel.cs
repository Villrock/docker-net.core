using System.Collections.Generic;

namespace QFlow.Models.ManageViewModels
{
    public class AlertTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsSendingEmail { get; set; }
        public IEnumerable<int> NotifyDays { get; set; }
    }
}
