using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CommentDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("authorId")]
        public int AuthorID { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("likes")]
        public List<LikeDTO> Likes { get; set; }
        [BsonElement("insertTime")]
        public BsonTimestamp InsertTime { get; set; }
        [BsonElement("updateTime")]
        public BsonTimestamp UpdateTime { get; set; }

    }
}
