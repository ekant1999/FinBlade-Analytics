using System;
using System.Collections.Generic;
using api.Data;
using api.Dtos.Comment;
using api.extensions;
using api.Interfaces;
using api.Mappers;
using api.Modles;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _commentRepository;
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ApplicationDBContext context, IStockRepository stockRepository,
                                UserManager<AppUser> userManager)
        {
            _context = context;
            _commentRepository = new CommentRepository(context);
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        // GET api/comment
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetComments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments =await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }

        // GET api/comment/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment =await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        // POST api/comment
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId ,[FromBody]  CreateCommentRequestDto commentModel)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!await _stockRepository.StockExistsAsync(stockId))
            {
                return BadRequest("Stock does not exist");
            }   
            
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var comment = commentModel.ToCommentFromCommentDto(stockId);
            comment.AppUserId = appUser.Id;
            var createdComment =await _commentRepository.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, createdComment);
        }

        // PUT api/comment/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute]int id, [FromBody] UpdateCommentReqDto commentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.UpdateCommentAsync(id, commentModel);

            if (comment == null)
            {
                return NotFound("comment not found");
            }
            
            return Ok(comment.ToCommentDto());
        }

        // DELETE api/comment/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok("Comment deleted successfully.");
        }
    }
}