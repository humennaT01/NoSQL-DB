using DTO.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALNeo4j.Interfaces
{
    public interface IUserDALn
    {
        void CreateUser(UserDTOn user);
        UserDTOn GetUser(int id);
        void UpdateUser(UserDTOn user);
        void DeleteUser(UserDTOn user);

        List<UserDTOn> GetUsers();

        void AddFriend(UserDTOn user, UserDTOn friend);
        void RemoveFriend(UserDTOn user, UserDTOn friend);
        bool IsFriends(UserDTOn user1, UserDTOn user2);
        List<UserDTOn> ShortestPath(int id1, int id2);
    }
}
