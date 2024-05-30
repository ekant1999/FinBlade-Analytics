using System;
using System.Collections.Generic;
using api.Data;
using api.Mappers;
using api.Modles;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using YourNamespace;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _commentRepository;
        private readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context)
        {
            _context = context;
            _commentRepository = new CommentRepository(context);
        }

        // GET api/comment
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments =await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(comments);
        }

        // GET api/comment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment =await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // POST api/comment
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            var createdComment =await _commentRepository.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, createdComment);
        }

        // PUT api/comment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _commentRepository.UpdateCommentAsync(id, comment);

            if (stockModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // DELETE api/comment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepository.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}