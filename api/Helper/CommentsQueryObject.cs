using System;

namespace api.Helper
{
    public class CommentsQueryObject
    {      
        public string? Symbol { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsSortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}