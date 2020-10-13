using DAL.Interfaces;
using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class PostDal : IPostDal
    {
        private string connectionString;

        public PostDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddComment(int id, CommentDTO comment)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var UpdateFilter = Builders<PostDTO>.Update.AddToSet("comments", comment);
                posts.UpdateOne(g => g.PostId == id, UpdateFilter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PostDTO CreatePost(PostDTO post)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var countId = posts.CountDocuments(p => p.PostId > 0);
                post.PostId = (int)countId + 1;
                posts.InsertOne(post);
                return post;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteComment(int postId, int commentId)
        {
            try
            {
                var post = this.GetPostById(postId);
                var comment = post.Comments[commentId - 1];
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var UpdateFilter = Builders<PostDTO>.Update.Pull("comments", comment);
                posts.UpdateOne(g => g.PostId == postId, UpdateFilter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                posts.DeleteOne(p => p.PostId == id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<PostDTO> GetAllPosts()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var allPosts = posts.Find(p => p.PostId > 0).ToList();
                return allPosts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PostDTO GetPostById(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var post = posts.Find(p => p.PostId == id).Single();
                return post;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Like(int postId)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var post = GetPostById(postId);
                post.Likes += 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UnLike(int postId)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var posts = db.GetCollection<PostDTO>("posts");
                var post = GetPostById(postId);
                post.Likes -= 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PostDTO UpdatePost(PostDTO post)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("social-network");
            var posts = db.GetCollection<PostDTO>("posts");
            int id = post.PostId;
            var oldPost = GetPostById(id);
            oldPost.UpdateTime = post.UpdateTime;
            oldPost.UserID = post.UserID;
            oldPost.Title = post.Title;
            oldPost.Likes = post.Likes;
            oldPost.Body = post.Body;
            oldPost.Comments = post.Comments;
            var res = posts.Find(p => p.PostId == post.PostId).Single();
            return res;

        }
    }
}
