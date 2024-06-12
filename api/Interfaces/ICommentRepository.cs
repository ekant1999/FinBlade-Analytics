using System.Collections.Generic;
using api.Dtos.Comment;
using api.Helper;
using api.Modles;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync(CommentsQueryObject commentsQueryObject);
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentReqDto comment);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}