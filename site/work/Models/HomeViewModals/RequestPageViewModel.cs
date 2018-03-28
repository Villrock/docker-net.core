namespace QFlow.Models.HomeViewModals
{
    public class RequestPageViewModel
    {
        public PagingInfo PageInfo { get; set; }
        public bool HasRequests { get; set; }
        public bool IsManager { get; set; }
        public int CurrentStatus { get; set; }
    }
}
