using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;


namespace DTO
{
    public class CommentDTO
    {
        [BsonElement("commentId")]
        public int CommentId { get; set; }
        [BsonElement("authorId")]
        public int AuthorID { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("likes")]
        public int Likes { get; set; }
        [BsonElement("insertTime")]
        public DateTime InsertTime { get; set; }
        [BsonElement("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
