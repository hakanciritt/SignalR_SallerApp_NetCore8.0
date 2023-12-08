using SignalR_App.Domain.Enums;

namespace SignalR_App.Application.Dtos.TestimonialDtos
{
    public class TestimonialDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public string? ImageUrl { get; set; }
        public Status Status { get; set; }
    }
}
