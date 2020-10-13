using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class PostDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("postId")]
        public int PostId { get; set; }
        [BsonElement("userId")]
        public int UserID { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("likes")]
        public int Likes { get; set; }
        [BsonElement("comments")]
        public List<CommentDTO> Comments { get; set; }
        [BsonElement("insertTime")]
        public DateTime InsertTime { get; set; }
        [BsonElement("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
