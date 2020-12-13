using DAL.Interfaces;
using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Concrete
{
    public class UserDal : IUserDal
    {
        private string connectionString;

        public UserDal()
        {
            this.connectionString = "mongodb://localhost:27017";
        }

        public UserDTO CreateUser(UserDTO user)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var users = db.GetCollection<UserDTO>("users");
                var countId = users.CountDocuments(p => p.UserId >= 0);
                user.UserId = (int)countId + 1;
                users.InsertOne(user);
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var users = db.GetCollection<UserDTO>("users");
                users.DeleteOne(p => p.UserId == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var users = db.GetCollection<UserDTO>("users");
                var allUsers = users.Find(p => p.UserId >= 0).ToList();
                return allUsers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UserDTO GetUserById(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var users = db.GetCollection<UserDTO>("users");
                var user = users.Find(p => p.UserId == id).Single();
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("social-network");
                var users = db.GetCollection<UserDTO>("users");
                int id = user.UserId;
                var oldUser = GetUserById(id);
                oldUser.LastName = user.LastName;
                oldUser.FirstName = user.FirstName;
                oldUser.Email = user.Email;
                oldUser.Friends = user.Friends;
                oldUser.Interests = user.Interests;
                oldUser.Login = user.Login;
                oldUser.Password = user.Password;
                var res = users.Find(p => p.UserId == user.UserId).Single();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
