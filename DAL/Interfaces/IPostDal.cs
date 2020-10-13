using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPostDal
    {
        PostDTO GetPostById(int id);
        List<PostDTO> GetAllPosts();
        PostDTO UpdatePost(PostDTO post);
        PostDTO CreatePost(PostDTO post);
        void DeletePost(int id);

        void Like(int postId);
        void UnLike(int postId);
        void AddComment(int id, CommentDTO comment);
        void DeleteComment(int postId, int commentId);
    }
}
