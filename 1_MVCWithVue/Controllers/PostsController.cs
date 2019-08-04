using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcWithVue.Models;
using MvcWithVue.Services;

namespace MvcWithVue.Controllers
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

        [HttpGet("new")]
        public ActionResult Create()
        {
            var model = new BlogPost
            {
                PostDate = DateTime.Now.Date
            };

            return View(model);
        }

        [HttpPost("new")]
        public ActionResult Create(BlogPost model)
        {
            try
            {
                _postService.Create(model);

                return RedirectToAction(nameof(ViewPost), new {slug = model.Slug });
            }
            catch(Exception)
            {
                return View(model);
            }
        }

        [HttpGet("edit/{id}")]
        public ActionResult Edit(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost("edit/{id}")]
        public ActionResult Edit(int id, BlogPost model)
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

                return RedirectToAction(nameof(ViewPost), new {slug = model.Slug });
            }
            catch(Exception)
            {
                return View(model);
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