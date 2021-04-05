using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameRoles;
using Owlgram.GameObjects;
using System.Timers;

namespace Owlgram.Game
{
    public class GameController
    {

        static int NUMBER_UNLIKED_POSTS_TO_EAT_MOUSE = 3;
        public List<User> Users = new List<User>();
        private Timer timerOwlNonEating= new Timer(30000.0);//30 seconds
        private Timer timerOwlNonPosting = new Timer(30000.0);//30 seconds
        private const int postingAmuletTime = 1;//minutes
        public GameController()
        {
            timerOwlNonEating.Elapsed += new ElapsedEventHandler(timerOwlNonEatingPunish);
            timerOwlNonEating.Start();

            timerOwlNonPosting.Elapsed += new ElapsedEventHandler(timerOwlNonPostingPunish);
            timerOwlNonPosting.Start();

            //timerNonPosting.Stop();
            //timerNonPosting.Dispose();
        }

        private void timerOwlNonPostingPunish(object sender, ElapsedEventArgs e)
        {
            foreach(User user in Users)
            {
                if (user is Owl owl)
                {
                    if ((DateTime.Now - owl.TimeOfLastPost).Minutes < postingAmuletTime)
                    {
                        return;
                    }

                    owl.NonPostingPunish();
                    if (!owl.IsLive)
                    {
                        Console.WriteLine($"Owl {owl.Name} is dead");
                        Users.RemoveAll(o => o == owl);
                    }
                }
            }
        }

        private void timerOwlNonEatingPunish(object sender, ElapsedEventArgs e)
        {
            foreach (User user in Users)
            {
                if (user is Owl owl)
                {
                    owl.NonEatingPunish();
                    if (!owl.IsLive)
                    {
                        Console.WriteLine($"Owl {owl.Name} is dead");
                        Users.RemoveAll(o => o == owl);
                    }
                }
            }
        }

        public void StartGame()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            Console.WriteLine("Select the action:");
            Console.WriteLine("1: Registration");
            if (Users.Count > 0)
                Console.WriteLine("2: Login");

            string response = Console.ReadLine();
            int itemNum = 0;

            if (itemNum > 2 || !int.TryParse(response, out itemNum))
            {
                Console.WriteLine("No such menu item");
                MainMenu();
                return;
            }

            switch (itemNum)
            {
                case 1:
                    Registration();
                    break;
                case 2:
                    UserEnter();
                    break;
                default:
                    Console.WriteLine("No such menu item");
                    break;
            }
        }

        private void UserEnter()
        {
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Password:");
            string pass = Console.ReadLine();


            User user = Users.FirstOrDefault(u => u.Password == pass && u.Name == name);
            if (user == null)
            {
                Console.WriteLine("\nNot right password or username\n");
                MainMenu();
                return;
            }

            if (user is Owl)
                OwlMenu((Owl)user);
            if (user is Mouse)
                MouseMenu((Mouse)user);
        }

        private void Registration()
        {
            Console.WriteLine("name:");
            string name = Console.ReadLine();

            foreach(User user in Users)
            {
                if(user.Name == name)
                {
                    Console.WriteLine("This name already taken :(");
                    Registration();
                    return;
                }
            }
            Console.WriteLine("password:");
            string pas = Console.ReadLine();
            Console.WriteLine("photo:");
            string photo = Console.ReadLine();
            Console.WriteLine("geo:");
            string geo = Console.ReadLine();

            Random rnd = new Random();
            if (Users.Count == 1)
            {
                if (Users[0] is Mouse)
                {
                    Owl owl = new Owl(name, pas, photo, geo);
                    Users.Add(owl);
                    OwlMenu(owl);
                    return;
                }
                else
                {
                    Mouse mouse = new Mouse(name, pas, photo, geo);
                    Users.Add(mouse);
                    MouseMenu(mouse);
                    return;
                }
            }
            if (rnd.Next(1, 2) == 1)
            {
                Owl owl = new Owl(name, pas, photo, geo);
                Users.Add(owl);
                OwlMenu(owl);
            }
            else
            {
                Mouse mouse = new Mouse(name, pas, photo, geo);
                Users.Add(mouse);
                MouseMenu(mouse);
            }
            return;
        }

        #region OwlMenu

        private void OwlMenu(Owl owl)
        {
            Console.WriteLine($"\nYou're owl: {owl.Name}\n  Glad to see you!\n  So you can:");
            Console.WriteLine("1: Publish the post");
            Console.WriteLine("2: Watch your state");
            Console.WriteLine("3: Hunt!");
            Console.WriteLine("4: Logout");

            while (true)
            {
                string response = Console.ReadLine();
                int itemNum = 0;

                if (itemNum > 2 || !int.TryParse(response, out itemNum))
                {
                    Console.WriteLine("No such menu item");
                    continue;
                }

                switch (itemNum)
                {
                    case 1:
                        PublishThePost(owl);
                        break;
                    case 2:
                        WatchOwlState(owl);
                        break;
                    case 3:
                        Hunt(owl);
                        break;
                    case 4:
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("No such menu item");
                        break;
                }
            }
        }

        private void WatchOwlState(Owl owl)
        {
            Console.WriteLine("Your states below:");
            Console.WriteLine($"    Happiness: {owl.Happiness}");
            Console.WriteLine($"    Satiety: {owl.Satiety}");
            Console.WriteLine($"    Eaten mice count: {owl.EatenMiceCount}");
        }

        private void Hunt(Owl owl)
        {
            Console.WriteLine("Let's check your targets!");
            List<Mouse> pretendentsToEat = owl.Observers;

            for (int i = 0; i < NUMBER_UNLIKED_POSTS_TO_EAT_MOUSE; i++)
            {
                foreach (Post post in owl.PublishedPosts)
                {
                    pretendentsToEat.Except(post.LikedMouses);
                }
            }

            foreach (Mouse mouse in pretendentsToEat)
            {
                owl.EatMouse(mouse);
                Console.WriteLine($"    {mouse.Name} was eaten");
                Users.RemoveAll(x => x == mouse && x is Mouse);
            }

            Console.WriteLine($"{pretendentsToEat.Count} mouses was eaten");
            Console.WriteLine($"Score is {owl.EatenMiceCount}");

            OwlMenu(owl);
        }

        private void PublishThePost(Owl owl)
        {
            Console.WriteLine("Enter the post data");
            Console.WriteLine("Text:");
            string text = Console.ReadLine();
            Console.WriteLine("Photo:");
            string photo = Console.ReadLine();
            Console.WriteLine("Geo:");
            string geo = Console.ReadLine();

            //сделать нотификацию
            owl.Uh(new Post(owl, text, geo, photo));

            OwlMenu(owl);
        }
        #endregion

        #region MouseMenu
        private void MouseMenu(Mouse mouse)
        {
            Console.WriteLine($"\nYou're mouse: {mouse.Name}\n  Glad to see you!\n  So you can:");
            Console.WriteLine("1: Watch unliked posts");
            Console.WriteLine("2: Subscribe to new owl");
            Console.WriteLine("3: Watch the state");
            Console.WriteLine("4: Add Telegram notifications");
            Console.WriteLine("5: Add WhatsApp notifications");
            Console.WriteLine("6: Add Viber notifications");
            Console.WriteLine("7: Add Email notifications");
            Console.WriteLine("8: Logout");

            while (true)
            {
                string response = Console.ReadLine();
                int itemNum = 0;

                if (itemNum > 8 || !int.TryParse(response, out itemNum))
                {
                    Console.WriteLine("No such menu item");
                    MouseMenu(mouse);
                    return;
                }

                switch (itemNum)
                {
                    case 1:
                        WatchUnlikedPosts(mouse);
                        break;
                    case 2:
                        SelectNewOwlToSubscribe(mouse);
                        break;
                    case 3:
                        WatchMouseState(mouse);
                        break;
                    case 4:
                        AddTelegramNotifications(mouse);
                        break;
                    case 5:
                        AddWhatsAppNotifications(mouse);
                        break;
                    case 6:
                        AddViberNotifications(mouse);
                        break;
                    case 7:
                        AddEmailNotifications(mouse);
                        break;
                    case 8:
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("No such menu item");
                        break;
                }
            }
        }

        private void WatchMouseState(Mouse mouse)
        {
            Console.WriteLine("Your states below:");
            Console.WriteLine($"    Life time (minutes): {mouse.LifeTimeFromMinutes}");
            Console.WriteLine($"    Count of liked posts: {mouse.LikedPostsCount}");
        }

        private void SelectNewOwlToSubscribe(Mouse mouse)
        {
            List<User> owls = Users.Where(u => u is Owl).ToList();

            List<Owl> notSubscriptionOwls = new List<Owl>();
            foreach(User userOwl in owls)
            {
                if (!mouse.Subscriptions.Contains((Owl)userOwl))
                    notSubscriptionOwls.Add((Owl)userOwl);
            }

            if(notSubscriptionOwls.Count ==0)
            {
                Console.WriteLine("No owl for subscribe :(");
                return;
            }

            Console.WriteLine("Enter the id of owl to subscribe: ");

            for (int i = 0; i < owls.Count; i++)
            {
                Console.WriteLine($"{i+1}: {owls[i].Name}");
            }

            string response = Console.ReadLine();
            int itemNum = 0;

            if (itemNum > owls.Count() || !int.TryParse(response, out itemNum))
            {
                Console.WriteLine("No such owl");
                SelectNewOwlToSubscribe(mouse);
                return;
            }

            //((Owl)owls[itemNum-1]).RegisterObserver(mouse);
            mouse.Subscribe(((Owl)owls[itemNum - 1]));
            Console.WriteLine($"You're subscribed to {((Owl)owls[itemNum - 1]).Name}");

            MouseMenu(mouse);
        }

        private void WatchUnlikedPosts(Mouse mouse)
        {
            List<Post> unlikedPosts = new List<Post>();
            foreach (Owl owl in mouse.Subscriptions)
            {
                foreach (Post post in owl.PublishedPosts)
                {
                    if (!post.LikedMouses.Contains(mouse))
                    {
                        unlikedPosts.Add(post);
                    }
                }
            }

            if(unlikedPosts.Count == 0)
            {
                Console.WriteLine("Good game. No unliked posts :)");
                return;
            }

            for(int i=0;i<unlikedPosts.Count;i++)
            {
                Console.Write($"{i+1}:    ");
                Console.WriteLine($"Text: {unlikedPosts[i].Text}  Photo: {unlikedPosts[i].Photo}  Geo: {unlikedPosts[i].Geo}");
            }

            Console.WriteLine("Enter the");
            Console.WriteLine("'id' to like the post");
            Console.WriteLine("'exit' to exit");

            while (true)
            {
                string response = Console.ReadLine();
                int itemNum = 0;

                if (response == "exit")
                {
                    MouseMenu(mouse);
                    return;
                }

                if (itemNum > unlikedPosts.Count() || !int.TryParse(response, out itemNum))
                {
                    Console.WriteLine("No such post");
                    continue;
                }
                itemNum--;
                mouse.Like(unlikedPosts[itemNum]);
                MouseMenu(mouse);
                return;
            }
        }

        public void AddEmailNotifications(Mouse mouse)
        {
            Console.WriteLine("Email notifications was added");
            mouse.AddEmailNotification();
        }

        public void AddTelegramNotifications(Mouse mouse)
        {
            Console.WriteLine("Telegram notifications was added");
            mouse.AddTelegramNotification();
        }

        public void AddViberNotifications(Mouse mouse)
        {
            Console.WriteLine("Viber notifications was added");
            mouse.AddViberNotification();
        }
        public void AddWhatsAppNotifications(Mouse mouse)
        {
            Console.WriteLine("WhatsApp notifications was added");
            mouse.AddWhatsAppNotification();
        }
        #endregion
    }
}
