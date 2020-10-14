using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Menu
{
    internal class UserMenu
    {
        private string connectionString;
        private PostDal postDal;
        private UserDal userDal;

        public UserMenu(string connectionString) {
            this.connectionString = connectionString;
            postDal = new PostDal(connectionString);
            userDal = new UserDal(connectionString);
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
            List<UserDTO> friends = getAllFriends(user);
            List<PostDTO> allPosts = postDal.GetAllPosts();
            List<PostDTO> post = new List<PostDTO>();

            foreach (PostDTO p in allPosts)
            {
                foreach (UserDTO u in friends)
                {
                    if (u.UserId == p.UserID) post.Add(p);
                }
            }

            if (post == null) Console.WriteLine("List of posts is empty, your friends don't write anything:(");
            else
            {
                showPosts(post);
                Console.WriteLine("\nYou can using postId: \n" +
                    "1. Add comment." +
                    "2. Like." +
                    "0. Exit.");
                int step;
                bool flag = true;
                while (flag)
                {
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
                                    if (id == p.PostId) postDal.Like(id);
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

        private void showFriends(List<UserDTO> friends)
        {
            Console.WriteLine("\n\t------Friends-----");
            Console.WriteLine($"{"-UserId-"}\t{"-Login-"}\t{"-FirstName-"}\t{"-LastName-"}");
            foreach (var f in friends)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t",
                        f.UserId, f.Login, f.FirstName, f.LastName);
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
                            user.FirstName = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Input new LastName: ");
                            user.LastName = Console.ReadLine();
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
            userDal.UpdateUser(user);
        }

        private List<UserDTO> getAllFriends(UserDTO user)
        {
            List<int> friendId = user.Friends;
            List<UserDTO> friends = new List<UserDTO>();
            foreach(int id in friendId)
            {
                friends.Add(userDal.GetUserById(id));
            }
            return friends;
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
                if (id == p.PostId) postDal.AddComment(id, com);
            }
        }

        private void addNewFriend(UserDTO user)
        {
            Console.WriteLine("Plese enter Login person you want to add to friendsList:");
            string login = Console.ReadLine();
            int id = 0;
            foreach (UserDTO u in userDal.GetAllUsers())
            {
                if (u.Login == login) id = u.UserId;
            }
            if (id != 0) user.Friends.Add(id);
            else Console.WriteLine("There are no user with that login!");
            userDal.UpdateUser(user);
        }

        private void deleteFriend(UserDTO user)
        {
            showFriends(getAllFriends(user));
            Console.WriteLine("Plese enter userId that you want to delete from your friend list:");
            int id = Convert.ToInt32(Console.ReadLine());
            int count = 0;
            foreach(UserDTO f in getAllFriends(user))
            {
                if(f.UserId == id) user.Friends.Remove(count);
                count++;
            }
            userDal.UpdateUser(user);
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
            postDal.CreatePost(post);
        }
    }
}