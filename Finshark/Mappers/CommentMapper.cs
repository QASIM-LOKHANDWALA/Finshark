using Finshark.Dtos.Comment;
using Finshark.Models;

namespace Finshark.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment comment)
        {
            return new CommentDTO 
            { 
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentDTO commentDTO, int stockId)
        {
            return new Comment
            {
                Title = commentDTO.Title,
                Content = commentDTO.Content,
                StockId = stockId,
            };
        }
    }
}
