using Finshark.Dtos.Comment;
using Finshark.Interface;
using Finshark.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finshark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();
            var commentDTO = comments.Select(c => c.ToCommentDTO());
            return Ok(commentDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment is null)
            {
                return NotFound("Comment does not exist.");
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockExists = await _stockRepo.StockExists(stockId);
            if (!stockExists)
            {
                return BadRequest("Stock does not exist.");
            }
            var comment = commentDTO.ToCommentFromCreateDTO(stockId);
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, commentDTO);
            if(comment is null)
            {
                return NotFound("Comment does not exist.");
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.DeleteAsync(id);
            if(comment is null)
            {
                return NotFound("Comment does not exist.");
            }
            return NoContent();
        }
    }
}
