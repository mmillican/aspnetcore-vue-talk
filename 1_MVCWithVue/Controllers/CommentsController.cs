using System;
using Microsoft.AspNetCore.Mvc;
using MvcWithVue.Models;
using MvcWithVue.Services;

namespace MvcWithVue.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/comments")]
    public class CommentsController : ControllerBase
    {        
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var comment = _commentService.GetById(id);

            return Ok(comment);
        }

        [HttpGet("")]
        public ActionResult GetByPost(int postId)
        {
            var comments = _commentService.GetByPostId(postId);

            return Ok(comments);
        }

        [HttpPost("")]
        public ActionResult Create(int postId, Comment model)
        {
            try
            {
                model.PostId = postId;
                model.Timestamp = DateTime.Now;
                
                _commentService.Create(model);

                return CreatedAtAction(nameof(GetById), new { postId, id = model.Id }, model);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("edit/{id}")]
        public ActionResult Edit(int postId, int id, Comment model)
        {
            var comment = _commentService.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            try
            {
                comment.Name = model.Name;
                comment.Text = model.Text;

                return Ok(model);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int postId, int id)
        {
            var comment = _commentService.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            try
            {
                _commentService.Delete(comment);

                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}