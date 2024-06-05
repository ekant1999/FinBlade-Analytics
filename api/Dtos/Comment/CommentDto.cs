using System;
using api.Modles;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy {get; set;} = string.Empty;
        public int? StockId { get; set; }
        
    }
}