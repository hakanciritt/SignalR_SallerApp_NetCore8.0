namespace SignalR_App.Application.Dtos.BookingDto
{
    public class BookingDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Mail { get; set; }
        public int PersonCount { get; set; }
        public DateTime Date { get; set; }
    }
}
