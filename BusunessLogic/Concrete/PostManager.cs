using BusunessLogic.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusunessLogic.Concrete
{
    public class PostManager : IPostManager
    {
        private readonly IPostDal _dal;
        public PostManager(IPostDal post)
        {
            _dal = post;
        }

        public void AddComment(int id, CommentDTO comment)
        {
            _dal.AddComment(id, comment);
        }

        public PostDTO CreatePost(PostDTO post)
        {
            return _dal.CreatePost(post);
        }

        public void DeleteComment(int postId, int commentId)
        {
            _dal.DeleteComment(postId, commentId);
        }

        public void DeletePost(int id)
        {
            _dal.DeletePost(id);
        }

        public List<PostDTO> GetAllPosts()
        {
            return _dal.GetAllPosts();
        }

        public PostDTO GetPostById(int id)
        {
            return _dal.GetPostById(id);
        }

        public void Like(int postId)
        {
            _dal.Like(postId);
        }

        public void UnLike(int postId)
        {
            _dal.UnLike(postId);
        }

        public PostDTO UpdatePost(PostDTO post)
        {
            return _dal.UpdatePost(post);
        }
    }
}
