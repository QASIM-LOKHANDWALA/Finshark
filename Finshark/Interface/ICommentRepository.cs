using Finshark.Dtos.Comment;
using Finshark.Models;

namespace Finshark.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> DeleteAsync(int id);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDTO commentDTO);
    }
}
