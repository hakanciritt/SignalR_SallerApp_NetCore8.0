using SignalR_App.WebUI.Areas.Admin.Models.Categories;

namespace SignalR_App.WebUI.Areas.Admin.Models.TextContents
{
    public class TextContentViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string Key { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ButtonCallToAction { get; set; }
        public string? ButtonName { get; set; }
        public int? MetaId { get; set; }
        public MetaViewModel Meta { get; set; }
    }
}
