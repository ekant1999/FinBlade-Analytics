using System.Collections.Generic;
using api.Modles;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(int id, Comment comment);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}