using System.ComponentModel.DataAnnotations;

namespace Finshark.Dtos.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long.")]
        [MaxLength(255, ErrorMessage = "Title cannot exceed 255 cgaracters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Content must be at least 2 characters long.")]
        [MaxLength(255, ErrorMessage = "Content cannot exceed 255 cgaracters.")]
        public string Content { get; set; } = string.Empty;
    }
}
