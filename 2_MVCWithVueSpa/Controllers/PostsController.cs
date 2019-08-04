using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVCWithVueSpa.Models;
using MVCWithVueSpa.Services;
using MVCWithVueSpa;

namespace MvvcWithVueSpa.Controllers
{
    [Route("posts")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            this._postService = postService;
        }

        [HttpGet("")]
        public ActionResult Index()
        {
            var posts = _postService.GetPosts().OrderByDescending(x => x.PostDate).ToList();

            return View(posts);
        }

        [HttpGet("posts")]
        public ActionResult Posts()
        {
            var posts = _postService.GetPosts().OrderByDescending(x => x.PostDate).ToList();
            return Ok(posts);
        }

        [HttpGet("id/{id}")]
        public ActionResult GetById(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet("{slug}")]
        public ActionResult ViewPost(string slug)
        {
            var post = _postService.GetBySlug(slug);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost("")]
        public ActionResult Create([FromBody] BlogPost model)
        {
            model.Id = _postService.GetPosts().Max(x => x.Id) + 1;

            _postService.Create(model);

            return Created("", model);
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, [FromBody] BlogPost model)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            try
            {               
                post.PostDate = model.PostDate;
                post.Slug = model.Slug;
                post.Title = model.Title;
                post.Content = model.Content;

                return Ok(model);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("suggest-slug")]
        // [Consumes("application/json")]
        public ActionResult SuggestSlug([FromBody] SuggestSlugModel model)
        {
            var slug = GenerateSlug(model.Title);
            return Json(new 
            {
                slug
            });
        }

        public class SuggestSlugModel 
        {
            public string Title { get; set; }
        }

        private string GenerateSlug(string title)
        {
            var slug = title.Clean();

            if (!DoesSlugExist(slug))
            {
                return slug;
            }
        
            var appendIdx = 1;
            var modSlug = $"{slug}-{appendIdx})";
            while (DoesSlugExist(modSlug))
            {
                appendIdx++;
                modSlug = $"{slug}-{appendIdx})";
            }

            return modSlug;
        }

        private bool DoesSlugExist(string slug) => _postService.DoesSlugExist(slug);
    }
}
