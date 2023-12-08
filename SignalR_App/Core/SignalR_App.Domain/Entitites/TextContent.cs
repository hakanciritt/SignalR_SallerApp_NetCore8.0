﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("TextContents")]
    public class TextContent : BaseFullEntity
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ButtonCallToAction { get; set; }
        public string? ButtonName { get; set; }

    }
}
