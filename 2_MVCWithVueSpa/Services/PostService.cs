using System;
using System.Collections.Generic;
using System.Linq;
using MVCWithVueSpa.Models;

namespace MVCWithVueSpa.Services
{
    public interface IPostService
    {
        List<BlogPost> GetPosts();

        BlogPost GetById(int id);
        BlogPost GetBySlug(string slug);

        bool DoesSlugExist(string slug);

        void Create(BlogPost post);
    }

    public class PostService : IPostService
    {        
        private static List<BlogPost> _posts = new List<BlogPost>
        {
            new BlogPost { Id = 1, PostDate = new DateTime(2019, 06, 01), Slug = "demo-post-1", Title = "Demo Post 1", Content = "Post 1 Content" },
            new BlogPost { Id = 2, PostDate = new DateTime(2019, 06, 15), Slug = "demo-post-2", Title = "Demo Post 2", Content = "Post 2 Content" },
            new BlogPost { Id = 3, PostDate = new DateTime(2019, 06, 20), Slug = "demo-post-3", Title = "Demo Post 3", Content = "Post 3 Content" },
        };

        public List<BlogPost> GetPosts() => _posts;

        public BlogPost GetById(int id) => _posts.SingleOrDefault(x => x.Id == id);

        public BlogPost GetBySlug(string slug) => _posts.SingleOrDefault(x => x.Slug == slug);

        public bool DoesSlugExist(string slug) => _posts.Any(x => x.Slug == slug);

        public void Create(BlogPost post) => _posts.Add(post);
    }
}