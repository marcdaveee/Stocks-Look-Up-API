using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket_with_Social_Media_Platform.Data;
using StockMarket_with_Social_Media_Platform.Dtos.Comment;
using StockMarket_with_Social_Media_Platform.Interfaces;
using StockMarket_with_Social_Media_Platform.Mappers;

namespace StockMarket_with_Social_Media_Platform.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentsController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsAsync()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();

            if(comments == null)
            {
                return NotFound();
            }

            var commentDtos = comments.Select(c => c.toCommentDto());

            return Ok(commentDtos);
        }

        [HttpGet]
        [Route("{commentId:int}")]

        public async Task <IActionResult> GetCommentById([FromRoute] int commentId)
        {
            var commentModel = await _commentRepo.GetComment(commentId);

            if(commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.toCommentDto());
           
        }


        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> CreateCommentAsync([FromRoute] int stockId, [FromBody] CreateCommentRequestDto createdComment )
        {
            if(!await _stockRepo.isStockExistAsync(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = createdComment.toCommentFromCreateDto(stockId);
                
            await _commentRepo.CreateCommentAsync(commentModel);

            return CreatedAtAction(nameof(GetCommentById), new { commentId = commentModel.Id }, commentModel.toCommentDto());
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updatedComment)
        {
            var commentModel = await _commentRepo.UpdateCommentAsync(id, updatedComment);

            if(commentModel == null)
            {   
                return NotFound("Comment Not Found");
            }

            return Ok(commentModel.toCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var commentModel = await _commentRepo.DeleteCommentAsync(id);

            if(commentModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return NoContent();
        }
    }

}
