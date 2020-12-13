using DALNeo4j.Interfaces;
using DTO.Neo4j;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALNeo4j.Concrete
{
    public class UserDALn : IUserDALn
    {
        private readonly IGraphClient _client;

        public UserDALn()
        {
            _client = new GraphClient(new Uri("http://localhost:7474"), "neo4j", "1111");
            _client.Connect();
        }

        public void AddFriend(UserDTOn user, UserDTOn friend)
        {
            _client.Cypher
                .Match("(u1:User),(u2:User)")
                .Where("u1.userId = {user_id1}")
                .AndWhere("u2.userId = {user_id2}")
                .WithParam("user_id1", user.userId)
                .WithParam("user_id2", friend.userId)
                .Create("(u1)-[:Friends]->(u2)")
                .ExecuteWithoutResults();
        }

        public void CreateUser(UserDTOn user)
        {
            _client.Cypher.Create("(u: User{ userId:{u1}, fisrtName:{u2}, lastName:{u3}, login:{u4}})")
                .WithParam("u1", user.userId)
                .WithParam("u2", user.firstName)
                .WithParam("u3", user.lastName)
                .WithParam("u3", user.login)
                .ExecuteWithoutResults();
        }

        public void DeleteUser(UserDTOn user)
        {
            _client.Cypher
                .Match("(u:User)-[f:FRIEND]-()")
                .Where("u.userId = {user_id}")
                .WithParam("user_id", user.userId)
                .Delete("f,u")
                .ExecuteWithoutResults();
        }

        public UserDTOn GetUser(int id)
        {
            var user = _client.Cypher
                .Match("(u:User)")
                .Where((UserDTOn u) => u.userId == id)
                .Return(u => u.As<UserDTOn>())
                .Results;
            UserDTOn result = new UserDTOn() {
                userId = id
            };
            foreach(var u in user)
            {
                result.userId = u.userId;
                result.lastName = u.lastName;
                result.login = u.login;
                result.firstName = u.firstName;
            }
            return result;
        }

        public bool IsFriends(UserDTOn user1, UserDTOn user2)
        {
            var isFriends = _client.Cypher
                 .Match("(user1:User)-[r:FRIEND]-(user2:User)")
                 .Where((UserDTOn u1) => u1.userId == user1.userId)
                 .AndWhere((UserDTOn u2) => u2.userId == user2.userId)
                 .Return(f => f.As<Friends>()).Results;
            if (isFriends.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public void RemoveFriend(UserDTOn user, UserDTOn friend)
        {
            _client.Cypher
                    .Match("(u1:User)-[f:FRIEND]-(u2:User)")
                    .Where("u1.userId = {user_id1}")
                    .AndWhere("u2.userId = {user_id2}")
                    .WithParam("user_id1", user.userId)
                    .WithParam("user_id2", friend.userId)
                    .Delete("f")
                    .ExecuteWithoutResults();
        }

        public List<UserDTOn> ShortestPath(int id1, int id2)
        {
            if (id1 == id2) { return new List<UserDTOn>(); }
            var path = _client.Cypher
                .Match("(u1:User{userId: {user_id1} }),(u2:User{userId: {user_id2} }),p = shortestPath((u1)-[:FRIEND*]-(u2))")
                .WithParam("user_id1", id1)
                .WithParam("user_id2", id2)
                .Return((r, len) => new
                {
                    shortestPath = Return.As<IEnumerable<Node<UserDTOn>>>("nodes(p)")
                }).Results;
            List<UserDTOn> result = new List<UserDTOn>();
            foreach (var step in path)
            {
                foreach (var s in step.shortestPath.ToList()) { result.Add(s.Data); }
            }
            return result;
        }

        public List<UserDTOn> GetFriends(int id)
        {
            return _client.Cypher
                .Match("(u:User)")
                .Match("(u)-[:FRIEND*1]-(f)")
                .Where((UserDTOn u) => u.userId == id)
                .Return(f => f.As<UserDTOn>())
                .Results.ToList();
        }

        public UserDTOn GetUserByLogin(string login)
        {
            var user = _client.Cypher
                   .Match("(u:User)")
                   .Where((UserDTOn u) => u.login == login)
                   .Return(u => u.As<UserDTOn>())
                   .Results;
            UserDTOn result = new UserDTOn()
            {
                login = login
            };
            foreach (var u in user)
            {
                result.userId = u.userId;
                result.lastName = u.lastName;
                result.login = u.login;
                result.firstName = u.firstName;
            }
            return result;
        }

        public void UpdateUser(UserDTOn user)
        {
            throw new NotImplementedException();
        }
    }
}
