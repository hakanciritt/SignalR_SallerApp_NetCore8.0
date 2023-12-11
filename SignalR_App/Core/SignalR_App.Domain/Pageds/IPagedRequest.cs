namespace SignalR_App.Domain.Pageds
{
    public interface IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
