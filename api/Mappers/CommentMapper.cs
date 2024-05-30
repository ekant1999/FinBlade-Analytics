using api.Modles;
using api.Dtos;
using api.Dtos.Comment;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title, 
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }

        // public static Comment ToCommentFromCreateCommentRequestDto(this CreateCommentRequestDto commentDto)
        // {
        //     return new Comment
        //     {
        //         Author = commentDto.Author,
        //         Body = commentDto.Body,
        //     };
        // }

        // public static Comment ToCommentFromUpdateCommentRequestDto(this UpdateCommentRequestDto commentDto)
        // {
        //     return new Comment
        //     {
        //         Author = commentDto.Author,
        //         Body = commentDto.Body,
        //     };
        // }
    }
}