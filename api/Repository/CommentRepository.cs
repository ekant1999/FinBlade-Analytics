using System.Collections.Generic;
using System.Runtime.CompilerServices;
using api.Data;
using api.Dtos.Comment;
using api.Helper;
using api.Interfaces;
using api.Modles;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext  context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(CommentsQueryObject commentsQueryObject)
        {
            var comment =  _context.Comments.Include(a=>a.AppUser).AsQueryable();
            if(!string.IsNullOrEmpty(commentsQueryObject.Symbol))
            {
                comment = comment.Where(a=>a.Stock != null && a.Stock.Symbol.ToLower() == commentsQueryObject.Symbol.ToLower());   
            }
            if(!string.IsNullOrWhiteSpace(commentsQueryObject.SortBy))
            {
                if(commentsQueryObject.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    comment = commentsQueryObject.IsSortDescending ? comment.OrderByDescending(s => s.Title) : comment.OrderBy(s => s.Title);
                }
            }
            return await comment.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.Include(a=>a.AppUser).FirstOrDefaultAsync(a=>a.Id == id );
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
             _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentReqDto commentReqDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(a=>a.Id == id);
            if (existingComment == null)
            {
                return null;
            }

             existingComment.Title = commentReqDto.Title;
             existingComment.Content = commentReqDto.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x=>x.Id == id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}