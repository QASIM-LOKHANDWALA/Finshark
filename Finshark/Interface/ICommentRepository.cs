using Finshark.Models;

namespace Finshark.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
