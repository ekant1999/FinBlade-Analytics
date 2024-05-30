using api.Modles;
using api.Dtos;
using api.Dtos.Comment;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Title = comment.Title, 
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }
        public static Comment ToCommentFromCommentDto(this CreateCommentRequestDto createCommentRequestDto, int stockId)
        {
            return new Comment
            {
                Title = createCommentRequestDto.Title,
                Content = createCommentRequestDto.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFrompdateCommentDto(this UpdateCommentReqDto updateCommentReqDto)
        {
            return new Comment
            {
                Title = updateCommentReqDto.Title,
                Content = updateCommentReqDto.Content
            };
        }
    }
}