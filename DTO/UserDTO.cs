using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DTO
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("userId")]
        public int UserId { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("login")]
        public string Login { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("interests")]
        public List<string> Interests { get; set; }
        [BsonElement("friends")]
        public List<int> Friends { get; set; }
    }
}
