using Finshark.Data;
using Finshark.Dtos.Comment;
using Finshark.Helpers;
using Finshark.Interface;
using Finshark.Models;
using Microsoft.EntityFrameworkCore;

namespace Finshark.Repository
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<List<Comment>> GetAllAsync(CommentQueryObject query)
        {
            var comments = _context.Comments.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                comments = comments.Where(c => c.Title.Contains(query.Title));
            }
            if (!string.IsNullOrWhiteSpace(query.Content))
            {
                comments = comments.Where(c => c.Content.Contains(query.Content));
            }
            comments = query.IsDecending == true ? comments.OrderByDescending(c => c.CreatedOn) : comments.OrderBy(c => c.CreatedOn);
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(comment is null)
            {
                return null;
            }
            return comment;
        }
        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(comment is null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDTO commentDTO)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment is null)
            {
                return null;
            }

            comment.Content = commentDTO.Content;
            comment.Title = commentDTO.Title;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
