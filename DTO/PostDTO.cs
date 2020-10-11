using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PostDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("userId")]
        public int UserID { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("likes")]
        public List<LikeDTO> Likes { get; set; }
        [BsonElement("comments")]
        public List<CommentDTO> Comments { get; set; }
        [BsonElement("insertTime")]
        public BsonTimestamp InsertTime { get; set; }
        [BsonElement("updateTime")]
        public BsonTimestamp UpdateTime { get; set; }
    }
}
