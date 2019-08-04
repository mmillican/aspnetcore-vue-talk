using System.Collections.Generic;
using System.Linq;
using MvcWithVue.Models;

namespace MvcWithVue.Services
{
    public interface ICommentService
    {
        List<Comment> GetByPostId(int postId);

        Comment GetById(int id);

        void Create(Comment comment);
        void Delete(Comment comment);
    }

    public class CommentService : ICommentService
    {
        private static readonly List<Comment> _comments = new List<Comment>
        {
            new Comment { Id = 1, PostId = 1, Timestamp = new System.DateTime(2019,06,01, 17, 0, 0), Name = "Commenter 1", Text = "This is a comment" },
            new Comment { Id = 2, PostId = 2, Timestamp = new System.DateTime(2019,07,04, 17, 0, 0), Name = "Commenter 2", Text = "This is a comment" }
        };

        public List<Comment> GetByPostId(int postId) => _comments.Where(x => x.PostId == postId).ToList();

        public Comment GetById(int id) => _comments.FirstOrDefault(x => x.Id == id);

        public void Create(Comment comment) => _comments.Add(comment);

        public void Delete(Comment comment) => _comments.Remove(comment);
    }
}