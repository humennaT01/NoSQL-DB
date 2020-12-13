using BusunessLogic.Concrete;
using BusunessLogic.Interfaces;
using DAL.Concrete;
using DTO;
using DTO.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Menu
{
    internal class UserMenu
    {
        private IAuthManager _authManager;
        private IUserManager _userManager;
        private IPostManager _postManager;

        public UserMenu(IAuthManager authManager, IUserManager userManager, IPostManager postManager) {
            _authManager = authManager;
            _userManager = userManager;
            _postManager = postManager;
        }

        public void start(UserDTO user)
        {
            getPosts(user);
            int step;
            bool flag = true;

            menuList();
            while (flag)
            {
                try
                {
                    step = Convert.ToInt32(Console.ReadLine());
                    switch (step)
                    {
                        case 1:
                            updating(user);
                            menuList();
                            break;
                        case 2:
                            showFriends(getAllFriends(user));
                            menuList();
                            break;
                        case 3:
                            addNewFriend(user);
                            menuList();
                            break;
                        case 4:
                            deleteFriend(user);
                            menuList();
                            break;
                        case 5:
                            getPosts(user);
                            menuList();
                            break;
                        case 6:
                            addNewPost(user.UserId);
                            break;
                        case 0:
                            flag = false;
                            break;
                        default:
                            throw new Exception("You've inputed wrong data.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Try again, or input 0 to exit.");
                }
            }
        }



        private void getPosts(UserDTO user)
        {
            List<UserDTOn> friends = _userManager.GetFriends(_userManager.GetUserByMongoId(user.UserId).userId);
            List<PostDTO> allPosts = _postManager.GetAllPosts();
            List<PostDTO> post = new List<PostDTO>();

            foreach (PostDTO p in allPosts)
            {
                foreach (UserDTOn u in friends)
                {
                    if (_userManager.GetUserByNeoId(u.userId).UserId == p.UserID) post.Add(p);
                }
            }

            if (post == null) Console.WriteLine("List of posts is empty, your friends don't write anything:(");
            else
            {
                showPosts(post);
                int step;
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("\nYou can using postId to: " +
                    "\n1. Add comment." +
                    "\n2. Like." +
                    "\n0. Exit." +
                    "\nSo, enter postId : ");
                    try
                    {
                        step = Convert.ToInt32(Console.ReadLine());
                        int id;
                        switch (step)
                        {
                            case 1:
                                id = Convert.ToInt32(Console.ReadLine());
                                addComment(user, post, id);
                                break;
                            case 2:
                                id = Convert.ToInt32(Console.ReadLine());
                                foreach(PostDTO p in post){
                                    if (id == p.PostId) _postManager.Like(id);
                                }
                                break;
                            case 0:
                                flag = false;
                                break;
                            default:
                                throw new Exception("You've inputed wrong data.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private void showPosts(List<PostDTO> posts)
        {
            Console.WriteLine("\n\t------Posts-----");
            Console.WriteLine($"{"-PostId-"}\t{"-UserId-"}\t{"-Title-"}\t{"-Body-"}\t{"-Likes-"}");
            foreach (var p in posts)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}",
                        p.PostId, p.UserID, p.Title, p.Body, p.Likes);
            }
            Console.WriteLine("\n");
        }

        private void showFriends(List<UserDTOn> friends)
        {
            Console.WriteLine("\n\t------Friends-----");
            Console.WriteLine($"{"-UserId-"}\t{"-Login-"}\t{"-FirstName-"}\t{"-LastName-"}");
            foreach (var f in friends)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t",
                        f.userId, f.login, f.firstName, f.lastName);
            }
            Console.WriteLine("\n");
        }

        private void menuList()
        {
            Console.WriteLine("\t\t~MENU~");
            Console.WriteLine("1. Update prifile");
            Console.WriteLine("2. Show all friends");
            Console.WriteLine("3. Add new friend.");
            Console.WriteLine("4. Delete friend.");
            Console.WriteLine("5. See all posts again.");
            Console.WriteLine("6. Add new post.");
            Console.WriteLine("0. Exit.");
        }

        private void updating(UserDTO user)
        {
            UserDTOn neoUser = _userManager.GetUserByMongoId(user.UserId);
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nPress number:\n\t1. to change FirstName;" +
                    "\n\t2. to change LastName;\n\t3. to change Email;\n\t4. to add new Interests;" +
                    "\n\t5. to change password;\n\t0.Exit\n");
                try
                {
                    int step = Convert.ToInt32(Console.ReadLine());
                    if (step == 0) flag = false;
                    switch (step)
                    {
                        case 1:
                            Console.WriteLine("Input new FirstName: ");
                            string firstName = Console.ReadLine();
                            user.FirstName = firstName;
                            neoUser.firstName = firstName;
                            break;
                        case 2:
                            Console.WriteLine("Input new LastName: ");
                            string lastName = Console.ReadLine();
                            user.LastName = lastName;
                            neoUser.lastName = lastName;
                            break;
                        case 3:
                            Console.WriteLine("Input new Email: ");
                            user.Email =Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Input new Interests: ");
                            string interests = Console.ReadLine();
                            user.Interests.Add(interests);
                            break;
                        case 5:
                            Console.WriteLine("Input new Password: ");
                            user.Password = Console.ReadLine();
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
            _userManager.UpdateUser(user,neoUser);
        }

        private List<UserDTOn> getAllFriends(UserDTO user)
        {
            int id = _userManager.GetUserByMongoId(user.UserId).userId;
            return _userManager.GetFriends(id);
        }

        private void addComment(UserDTO user, List<PostDTO> post, int id)
        {
            Console.WriteLine("Enter your comment");
            string comment = Console.ReadLine();
            CommentDTO com = new CommentDTO();
            com.AuthorID = user.UserId;
            com.InsertTime = DateTime.UtcNow;
            com.Likes = 0;
            com.UpdateTime = DateTime.UtcNow;
            com.Text = comment;
            foreach (PostDTO p in post)
            {
                if (id == p.PostId) _postManager.AddComment(id, com);
            }
        }

        private void addNewFriend(UserDTO user)
        {
            Console.WriteLine("Plese enter Login person you want to add to friendsList:");
            string login = Console.ReadLine();
            UserDTOn neoUser = _userManager.GetUserByMongoId(user.UserId);
            UserDTOn friend = _userManager.GetUserByLogin(login);
            _userManager.AddFriend(neoUser, friend);
        }

        private void deleteFriend(UserDTO user)
        {
            showFriends(getAllFriends(user));
            Console.WriteLine("Plese enter userId that you want to delete from your friend list:");
            int id = Convert.ToInt32(Console.ReadLine());
            UserDTOn neoUser = _userManager.GetUserByMongoId(user.UserId);
            UserDTOn friend = _userManager.GetUserNeo4j(id);
            _userManager.RemoveFriend(neoUser, friend);
        }

        private void addNewPost(int userId)
        {
            PostDTO post = new PostDTO();
            Console.WriteLine("Enter Title: ");
            post.Title = Console.ReadLine();
            Console.WriteLine("Enter Text: ");
            post.Body = Console.ReadLine();
            post.Likes = 0;
            post.Comments = new List<CommentDTO>();
            post.InsertTime = DateTime.UtcNow;
            post.UpdateTime = DateTime.UtcNow;
            post.UserID = userId;
            _postManager.CreatePost(post);
        }
    }
}