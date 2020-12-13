using BusunessLogic.Concrete;
using BusunessLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;
using DALNeo4j.Concrete;
using DALNeo4j.Interfaces;
using DTO;
using DTO.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Menu
{
    public class MainMenu
    {
        private IAuthManager _authManager;
        private IUserManager _userManager;
        private IPostManager _postManager;

        public MainMenu() {
            IUserDal userM = new UserDal();
            IPostDal post = new PostDal();
            IUserDALn userN = new UserDALn();
            _authManager = new AuthManager(userM);
            _userManager = new UserManager(userM, userN);
            _postManager = new PostManager(post);
        }

        public void startWork()
        {
            Console.WriteLine("\t\t\t~START~");
            Console.WriteLine("Choose what do you want to do: " +
                "\n1. Log in." +
                "\n2. Create new Account." +
                "\n0. Exit.");
            int step;
            bool flag = true;
            UserMenu menu = new UserMenu(_authManager, _userManager, _postManager);
            while (flag)
            {
                try
                {
                    step = Convert.ToInt32(Console.ReadLine());
                    switch (step)
                    {
                        case 1:
                            UserDTO user = enterLogin();
                            menu.start(user);
                            break;
                        case 2:
                            UserDTO u = createNewAccount();
                            menu.start(u);
                            break;
                        case 0:
                            flag = false;
                            break;
                        default:
                            throw new Exception("You've inputed wrong data.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Try again, or input 0 to exit.");
                }
            }
        }

        private UserDTO createNewAccount()
        {
            UserDTO user = new UserDTO();
            UserDTOn userNeo = new UserDTOn();

            Console.WriteLine("\t\t~NEW USER~ ");
            Console.WriteLine("Input FirstName: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Input LastName: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Input Email:");
            user.Email = Console.ReadLine();

            user.FirstName = firstName;
            user.LastName = lastName;
            userNeo.firstName = firstName;
            userNeo.lastName = lastName;

            bool flag = true;
            string login;
            
            while (flag)
            {
                try
                {
                    Console.WriteLine("Input Login:");
                    login = Console.ReadLine();
                    List<UserDTO> users = _userManager.GetAllUsersM();
                    bool isFind = false;
                    foreach (UserDTO u in users)
                    {
                        if (u.Login == login)
                        {
                            isFind = true;
                            throw new Exception();
                        }
                    }
                    if (isFind == false)
                    {
                        user.Login = login;
                        userNeo.login = login;
                        flag = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("User with that login already exists!\nTry again!");
                }
            }
            Console.WriteLine("Input Password:");
            user.Password = Console.ReadLine();

            user.Interests = new List<string>();
            user.Friends = new List<int>();

            _userManager.CreateUser(userNeo, user);
            return user;
        }

        private UserDTO enterLogin()
        {
            bool flag = true;
            string login;
            string pass;
            UserDTO user = new UserDTO();
            while (flag)
            {
                try
                {
                    Console.Write("Please enter your login: ");
                    login = Console.ReadLine();
                    Console.Write("Please enter password: ");
                    pass = Console.ReadLine();
                    user = _authManager.LogIn(login, pass);
                    if (user == null) throw new Exception();
                    else
                    {
                        return user;
                        flag = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Sorry, login or email is wrong!"
                        + "\nTry again.");
                }
            }
            return user;
        }
    }
}
