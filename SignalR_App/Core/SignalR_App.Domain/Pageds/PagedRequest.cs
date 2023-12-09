namespace SignalR_App.Domain.Pageds
{
    public class PagedRequest : IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
