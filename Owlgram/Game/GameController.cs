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
        private Timer timerOwlNonEating = new Timer(30000.0);//30 seconds
        private Timer timerOwlNonPosting = new Timer(30000.0);//30 seconds
        private const int POSTING_IMMUNITY_TIME = 1;//время сохранения показателей в исходном состоянии после публикации поста в минутах
        private delegate void MenuItemCommand(User user = null);
        private Dictionary<int, MenuItemCommand> MainMenuItems
        {
            get => new Dictionary<int, MenuItemCommand>() 
            {
                {1, Register },
                {2, UserEnter }
            };
        }

        private Dictionary<int, MenuItemCommand> MouseMenuItems
        {

            get => new Dictionary<int, MenuItemCommand>()
            {
                {1, WatchUnlikedPosts },
                {2, SelectNewOwlToSubscribe },
                {3, WatchMouseState },
                {4, AddTelegramNotifications },
                {5, AddWhatsAppNotifications },
                {6, AddViberNotifications },
                {7, AddEmailNotifications },
                {8, MainMenu },
            };
        }

        private Dictionary<int, MenuItemCommand> OwlMenuItems
        {
            get => new Dictionary<int, MenuItemCommand>()
            {
                {1, PublishThePost },
                {2, WatchOwlState },
                {3, Hunt },
                {4, MainMenu },
            };

        }

        public GameController()
        {
            timerOwlNonEating.Elapsed += new ElapsedEventHandler(TimerOwlNonEatingPunish);
            timerOwlNonEating.Start();

            timerOwlNonPosting.Elapsed += new ElapsedEventHandler(TimerOwlNonPostingPunish);
            timerOwlNonPosting.Start();
        }
        #region Timers
        private void TimerOwlNonPostingPunish(object sender, ElapsedEventArgs e)
        {
            foreach (User user in Users)
            {
                if (user is Owl owl)
                {
                    if ((DateTime.Now - owl.TimeOfLastPost).Minutes < POSTING_IMMUNITY_TIME)
                    {
                        return;
                    }

                    owl.NonPostingPunish();
                    DisplayIfOwlIsDeadInfo(owl);
                }
            }
        }

        private void TimerOwlNonEatingPunish(object sender, ElapsedEventArgs e)
        {
            foreach (User user in Users)
            {
                if (user is Owl owl)
                {
                    owl.NonEatingPunish();
                    DisplayIfOwlIsDeadInfo(owl);
                }
            }
        }
        private void DisplayIfOwlIsDeadInfo(Owl owl)
        {
            if (!owl.IsLive)
            {
                Console.WriteLine($"Owl '{owl.Name}' is dead");
                Users.RemoveAll(o => o == owl);
            }
        }
        #endregion
        private void TryExecute(Dictionary<int, MenuItemCommand> menuItems, User user = null)
        {
            while (true)
            {
                string response = Console.ReadLine();
                int itemNum;
                int.TryParse(response, out itemNum);

                if (!MouseMenuItems.Keys.Contains(itemNum))
                {
                    Console.WriteLine("No such menu item");
                    continue;
                }

                menuItems[itemNum](user);
            }
        }

        public void StartGame()
        {
            MainMenu();
        }

        #region MainMenu
        private void MainMenu(User noUser = null)
        {
            Console.WriteLine("Select the action:");
            Console.WriteLine("1: Registration");
            if (Users.Count > 0)
                Console.WriteLine("2: Login");

            TryExecute(MainMenuItems);
        }

        private void UserEnter(User noUser)
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

        private void Register(User noUser = null)
        {
            Console.WriteLine("Name:");
            string name = Console.ReadLine();

            foreach (User user in Users)
            {
                if (user.Name == name)
                {
                    Console.WriteLine("This name already taken :(");
                    Register();
                    return;
                }
            }
            Console.WriteLine("Password:");
            string pas = Console.ReadLine();
            Console.WriteLine("Photo:");
            string photo = Console.ReadLine();
            Console.WriteLine("Geo:");
            string geo = Console.ReadLine();

            Random rnd = new Random(DateTime.Now.Second);
            if (Users.Count == 1)
            {
                if (Users[0] is Mouse)
                {
                    RegisterNewOwl(name, pas, photo, geo);
                    return;
                }
                else
                {
                    RegisterNewMouse(name, pas, photo, geo);
                    return;
                }
            }
            if (rnd.Next(1, 3) == 1)
                RegisterNewOwl(name, pas, photo, geo);
            else
                RegisterNewMouse(name, pas, photo, geo);

            return;
        }

        private void RegisterNewOwl(string name, string password, string photo, string geo)
        {
            Owl owl = new Owl(name, password, photo, geo);
            Users.Add(owl);
            OwlMenu(owl);
        }

        private void RegisterNewMouse(string name, string password, string photo, string geo)
        {
            Mouse mouse = new Mouse(name, password, photo, geo);
            Users.Add(mouse);
            MouseMenu(mouse);
        }
        #endregion

        #region OwlMenu
        private void OwlMenu(Owl owl)
        {
            Console.WriteLine($"\nYou're owl: '{owl.Name}'\n  Glad to see you!\n  So you can:");
            Console.WriteLine("1: Publish the post");
            Console.WriteLine("2: Watch your state");
            Console.WriteLine("3: Hunt!");
            Console.WriteLine("4: Logout");

            TryExecute(OwlMenuItems, owl);
        }

        private void WatchOwlState(User user)
        {
            if (user is Owl owl)
            {
                Console.WriteLine("Your states below:");
                Console.WriteLine($"    Happiness: {owl.Happiness}");
                Console.WriteLine($"    Satiety: {owl.Satiety}");
                Console.WriteLine($"    Eaten mice count: {owl.EatenMiceCount}");
            }
            else
                return;
        }

        private void Hunt(User user) //охота
        {
            if (user is Owl owl)
            {
                Console.WriteLine("Let's check your targets!");
                List<Mouse> eatenMice = owl.Hunt(NUMBER_UNLIKED_POSTS_TO_EAT_MOUSE);

                foreach (Mouse mouse in eatenMice)
                {
                    Console.WriteLine($"    '{mouse.Name}' was eaten");
                    Users.RemoveAll(x => x == mouse && x is Mouse);
                }

                Console.WriteLine($"{eatenMice.Count} mouses was eaten");
                Console.WriteLine($"Score is {owl.EatenMiceCount}");

                OwlMenu(owl);
            }
        }

        private void PublishThePost(User user)
        {
            if (user is Owl owl)
            {
                Console.WriteLine("Enter the post data");
                Console.WriteLine("Text:");
                string text = Console.ReadLine();
                Console.WriteLine("Photo:");
                string photo = Console.ReadLine();
                Console.WriteLine("Geo:");
                string geo = Console.ReadLine();

                owl.Uh(new Post(owl, text, geo, photo));

                OwlMenu(owl);
            }
        }
        #endregion

        #region MouseMenu
        private void MouseMenu(Mouse mouse)
        {
            Console.WriteLine($"\nYou're mouse: '{mouse.Name}'\n  Glad to see you!\n  So you can:");
            Console.WriteLine("1: Watch unliked posts");
            Console.WriteLine("2: Subscribe to new owl");
            Console.WriteLine("3: Watch the state");
            Console.WriteLine("4: Add Telegram notifications");
            Console.WriteLine("5: Add WhatsApp notifications");
            Console.WriteLine("6: Add Viber notifications");
            Console.WriteLine("7: Add Email notifications");
            Console.WriteLine("8: Logout");

            TryExecute(MouseMenuItems, mouse);
        }
        
        private void WatchMouseState(User user)
        {
            Mouse mouse = (Mouse)user;
            Console.WriteLine("Your states below:");
            Console.WriteLine($"    Life time (minutes): {mouse.LifeTimeFromMinutes}");
            Console.WriteLine($"    Count of liked posts: {mouse.LikedPostsCount}");
        }

        private void SelectNewOwlToSubscribe(User user)
        {
            if (user is Mouse mouse)
            {
                List<User> owls = Users.Where(u => u is Owl).ToList();

                List<Owl> notSubscriptionOwls = new List<Owl>();
                foreach (User userOwl in owls)
                {
                    if (!mouse.Subscriptions.Contains((Owl)userOwl))
                        notSubscriptionOwls.Add((Owl)userOwl);
                }

                if (notSubscriptionOwls.Count == 0)
                {
                    Console.WriteLine("No owl for subscribe :(");
                    return;
                }

                Console.WriteLine("Enter the id of owl to subscribe: ");

                for (int i = 0; i < notSubscriptionOwls.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {notSubscriptionOwls[i].Name}");
                }

                string response = Console.ReadLine();
                int itemNum = 0;

                if (itemNum > notSubscriptionOwls.Count() || !int.TryParse(response, out itemNum))
                {
                    Console.WriteLine("No such owl");
                    SelectNewOwlToSubscribe(mouse);
                    return;
                }
                itemNum--;

                //((Owl)owls[itemNum-1]).RegisterObserver(mouse);
                mouse.Subscribe(((Owl)owls[itemNum]));
                Console.WriteLine($"You're subscribed to '{((Owl)owls[itemNum]).Name}'");

                MouseMenu(mouse);
            }
        }

        private void WatchUnlikedPosts(User user)
        {
            if (user is Mouse mouse)
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

                if (unlikedPosts.Count == 0)
                {
                    Console.WriteLine("Good game. No unliked posts :)");
                    return;
                }

                for (int i = 0; i < unlikedPosts.Count; i++)
                {
                    Console.Write($"{i + 1}:    ");
                    Console.WriteLine($"Text: {unlikedPosts[i].Text}  Photo: {unlikedPosts[i].Photo}  Geo: {unlikedPosts[i].Geo}");
                }

                Console.WriteLine("Enter the");
                Console.WriteLine("'id' to like the post");
                Console.WriteLine("'exit' to exit");

                while (true)
                {
                    string response = Console.ReadLine();

                    if (response == "exit")
                    {
                        MouseMenu(mouse);
                        return;
                    }

                    int itemNum;
                    int.TryParse(response, out itemNum);

                    if (itemNum > unlikedPosts.Count())
                    {
                        Console.WriteLine("No such post :(");
                        Console.WriteLine("Enter the right command");
                        continue;
                    }
                    itemNum--;
                    mouse.Like(unlikedPosts[itemNum]);
                    MouseMenu(mouse);
                    return;
                }
            }
        }

        public void AddEmailNotifications(User user)
        {
            Console.WriteLine("Email notifications was added");
            ((Mouse)user).AddEmailNotification();
        }

        public void AddTelegramNotifications(User user)
        {
            Console.WriteLine("Telegram notifications was added");
            ((Mouse)user).AddTelegramNotification();
        }

        public void AddViberNotifications(User user)
        {
            Console.WriteLine("Viber notifications was added");
            ((Mouse)user).AddViberNotification();
        }
        public void AddWhatsAppNotifications(User user)
        {
            Console.WriteLine("WhatsApp notifications was added");
            ((Mouse)user).AddWhatsAppNotification();
        }
        #endregion
    }
}
