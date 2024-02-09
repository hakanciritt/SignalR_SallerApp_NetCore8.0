namespace SignalR_App.Application.Dtos.ContactDtos
{
    public class ContactDto : BaseEntityDto
    {
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
