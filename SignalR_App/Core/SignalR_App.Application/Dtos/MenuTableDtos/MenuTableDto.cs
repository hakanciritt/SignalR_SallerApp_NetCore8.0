using SignalR_App.Application.Dtos.BasketDtos;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Enums;

namespace SignalR_App.Application.Dtos.MenuTableDtos
{
    public class MenuTableDto : BaseFullEntity
    {
        public string Name { get; set; }
        public Status Status { get; set; }

        public int? BasketId { get; set; }
        public BasketDto Basket { get; set; }
    }
}
