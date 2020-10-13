using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Menu
{
    public class MainMenu
    {
        private string connectionString = "mongodb://localhost:27017";

        public MainMenu() { }

        public void startWork()
        {
            Console.WriteLine("\t\t\t~START~");
            Console.WriteLine("Choose what do you want to do: " +
                "\n1. Log in." +
                "\n2. Create new Account." +
                "\n0. Exit.");
            int step;
            bool flag = true;
            UserMenu menu = new UserMenu(connectionString);
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
            UserDal dal = new UserDal(connectionString);
            UserDTO user = new UserDTO();

            Console.WriteLine("\t\t~NEW USER~ ");
            Console.WriteLine("Input FirstName: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Input LastName: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Input Email:");
            user.Email = Console.ReadLine();

            bool flag = true;
            string login;
            
            while (flag)
            {
                try
                {
                    Console.WriteLine("Input Login:");
                    login = Console.ReadLine();
                    List<UserDTO> users = dal.GetAllUsers();
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
            string password = Console.ReadLine();

            user.Interests = new List<string>();
            user.Friends = new List<int>();

            dal.CreateUser(user);
            return user;
        }

        private UserDTO enterLogin()
        {
            Console.Write("Please enter your login: ");
            bool flag = true;
            string login;
            UserDal dal = new UserDal(connectionString);
            UserDTO user = null;
            while (flag)
            {
                try
                {
                    login = Console.ReadLine();
                    List<UserDTO> users = dal.GetAllUsers();
                    bool isFind = false;
                    foreach (UserDTO u in users)
                    {
                        if (u.Login == login)
                        {
                            isFind = true;
                            user = u;
                            enterPassword(user);
                            flag = false;
                        }
                    }
                    if (isFind == false) throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Sorry, we can not find user with such login!"
                        + "\nTry again.");
                }
            }
            return user;
        }

        private void enterPassword(UserDTO user)
        {
            bool flag = true;
            string password;
            while (flag)
            {
                try
                {
                    Console.Write("Please enter password: ");
                    password = Console.ReadLine();
                    if (password == user.Password) flag = false;
                    else throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Sorry, password is wrong!"
                        + "\nTry again.");
                }
            }
        }

    }
}
