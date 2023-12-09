using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Dtos.TextContentDtos
{
    public class TextContentDto : BaseFullEntity
    {
        public string Key { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ButtonCallToAction { get; set; }
        public string? ButtonName { get; set; }
        public int? MetaId { get; set; }
        public MetaDto.MetaDto Meta { get; set; }
    }
}
