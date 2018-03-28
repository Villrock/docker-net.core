using System;
using System.Collections.Generic;

namespace QFlow.Models.NotificationViewModels
{
    public class NotificationViewModel
    {
        public IEnumerable<NotificationItemViewModel> Items { get; set; }
        public PagingInfo PageInfo { get; set; }
    }

    public class NotificationItemViewModel
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
