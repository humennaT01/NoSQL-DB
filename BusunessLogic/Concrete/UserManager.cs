using BusunessLogic.Interfaces;
using DAL.Interfaces;
using DALNeo4j.Interfaces;
using DTO;
using DTO.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusunessLogic.Concrete
{
    public class UserManager : IUserManager
    {
        private IUserDal _userM;
        private IUserDALn _userN;

        public UserManager(IUserDal userM, IUserDALn userN) {
            _userM = userM;
            _userN = userN;
        }

        public void AddFriend(UserDTOn user, UserDTOn friend)
        {
            _userN.AddFriend(user, friend);
        }

        public void CreateUser(UserDTOn userN, UserDTO userM)
        {
            _userM.CreateUser(userM);
            _userN.CreateUser(userN);
        }

        public void DeleteUser(UserDTOn userN, UserDTO userM)
        {
            _userM.DeleteUser(userM.UserId);
            _userN.DeleteUser(userN);
        }

        public List<UserDTO> GetAllUsersM()
        {
            return _userM.GetAllUsers();
        }

        public List<UserDTOn> GetFriends(int id)
        {
            return _userN.GetFriends(id);
        }

        public UserDTOn GetUserByMongoId(int id)
        {
            UserDTO user = _userM.GetUserById(id);
            return _userN.GetUserByLogin(user.Login);
        }

        public UserDTO GetUserByNeoId(int id)
        {
            UserDTOn user = _userN.GetUser(id);
            return _userM.GetAllUsers().Where(u => u.Login == user.login).FirstOrDefault();
        }

        public UserDTO GetUserMongoDB(int id)
        {
            return _userM.GetUserById(id);
        }

        public UserDTOn GetUserNeo4j(int id)
        {
            return _userN.GetUser(id);
        }

        public bool IsFriends(UserDTOn user1, UserDTOn user2)
        {
            return _userN.IsFriends(user1,user2);
        }

        public void RemoveFriend(UserDTOn user, UserDTOn friend)
        {
            _userN.RemoveFriend(user, friend);
        }

        public List<UserDTOn> ShortestPath(int id1, int id2)
        {
            return _userN.ShortestPath(id1, id2);
        }

        public UserDTOn GetUserByLogin(string login)
        {
            return _userN.GetUserByLogin(login);
        }

        public void UpdateUser(UserDTO userM, UserDTOn userN)
        {
            _userM.UpdateUser(userM);
            _userN.UpdateUser(userN);
        }
    }

}
