using BusunessLogic.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusunessLogic.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserDal _manager;
        public AuthManager(IUserDal manager)
        {
            _manager = manager;
        }

        public UserDTO LogIn(string username, string pass)
        {
            return _manager.GetAllUsers().Where(u => u.Login == username && u.Password == pass).FirstOrDefault();
        }
    }
}
