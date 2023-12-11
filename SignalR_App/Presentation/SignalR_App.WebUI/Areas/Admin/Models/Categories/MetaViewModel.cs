namespace SignalR_App.WebUI.Areas.Admin.Models.Categories
{
    public class MetaViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
    }
}
