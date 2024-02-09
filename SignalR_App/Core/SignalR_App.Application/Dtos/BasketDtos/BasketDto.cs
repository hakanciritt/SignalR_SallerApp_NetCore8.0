using SignalR_App.Domain.Enums;

namespace SignalR_App.Application.Dtos.BasketDtos
{
    public class BasketDto : BaseFullEntityDto
    {
        public int TotalPrice { get; set; }
        public int MenuTableId { get; set; }

        public Status Status { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; }
    }
}

