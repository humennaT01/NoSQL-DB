using MongoDB.Driver;
using SocialNetwork.Menu;
using System;

namespace SocialNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.startWork();
        }
    }
}
