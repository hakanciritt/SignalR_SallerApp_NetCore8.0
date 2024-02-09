namespace SignalR_App.Application.Dtos.NotificationDtos
{
    public class NotificationDto : BaseEntityDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
    }
}
