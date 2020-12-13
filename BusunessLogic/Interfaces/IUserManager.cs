using DTO;
using DTO.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusunessLogic.Interfaces
{
    public interface IUserManager
    {
        //Common
        void CreateUser(UserDTOn userN, UserDTO userM);
        void DeleteUser(UserDTOn userN, UserDTO userM);
        void UpdateUser(UserDTO userM, UserDTOn userN);

        //MongoDB
        UserDTO GetUserMongoDB(int id);
        List<UserDTO> GetAllUsersM();
        UserDTO GetUserByNeoId(int id);

        //Neo4j
        UserDTOn GetUserNeo4j(int id);
        void AddFriend(UserDTOn user, UserDTOn friend);
        void RemoveFriend(UserDTOn user, UserDTOn friend);
        bool IsFriends(UserDTOn user1, UserDTOn user2);
        List<UserDTOn> ShortestPath(int id1, int id2);
        List<UserDTOn> GetFriends(int id);
        UserDTOn GetUserByMongoId(int id);
        UserDTOn GetUserByLogin(string login);
    }
}
