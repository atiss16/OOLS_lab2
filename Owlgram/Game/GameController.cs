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
        private List<Owl> Owls => Users.Where(u => u is Owl).Select(u => (Owl)u).ToList<Owl>();
        private Dictionary<int, MenuItem> MainMenuItems => new Dictionary<int, MenuItem>() 
        {
            {1, new MenuItem("Regstration", Register) },
            {2, new MenuItem("Login", UserEnter) }
        };
        private Dictionary<int, MenuItem> MouseMenuItems => new Dictionary<int, MenuItem>()
        {
            {1, new MenuItem("Watch unliked posts", WatchUnlikedPosts) },
            {2, new MenuItem("Subscribe to new owl", SelectNewOwlToSubscribe) },
            {3, new MenuItem("Watch the state", WatchMouseState) },
            {4, new MenuItem("Add Telegram notifications", AddTelegramNotifications) },
            {5, new MenuItem("Add WhatsApp notifications", AddWhatsAppNotifications) },
            {6, new MenuItem("Add Viber notifications", AddViberNotifications) },
            {7, new MenuItem("Add Email notifications", AddEmailNotifications) },
            {8, new MenuItem("Logout", MainMenu) },
        };

        private Dictionary<int, MenuItem> OwlMenuItems => new Dictionary<int, MenuItem>()
        {
            {1, new MenuItem("Publish the post", PublishThePost )},
            {2, new MenuItem("Watch your state", WatchOwlState) },
            {3, new MenuItem("Hunt!", Hunt) },
            {4, new MenuItem("Logout", MainMenu) },
        };

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
            foreach (Owl owl in Owls)
            {
                if ((DateTime.Now - owl.TimeOfLastPost).Minutes < POSTING_IMMUNITY_TIME)
                {
                    return;
                }

                owl.NonPostingPunish();
                DisplayIfOwlIsDeadInfo(owl);
            }
        }

        private void TimerOwlNonEatingPunish(object sender, ElapsedEventArgs e)
        {
            foreach (Owl owl in Owls)
            {
                owl.NonEatingPunish();
                    DisplayIfOwlIsDeadInfo(owl);
            }
        }
        private void DisplayIfOwlIsDeadInfo(Owl owl)
        {
            if (!owl.IsLive)
            {
                Console.WriteLine($"Owl '{owl.Name}' is dead");
                Users.Remove(owl);
            }
        }
        #endregion
        private void TryExecute(Dictionary<int, MenuItem> menuItems, User user = null)
        {
            foreach(int numberItem in menuItems.Keys)
            {
                Console.WriteLine($"{numberItem}: {menuItems[numberItem].Description}");
            }
            while (true)
            {
                string response = Console.ReadLine();
                int itemNum;
                int.TryParse(response, out itemNum);

                if (!menuItems.Keys.Contains(itemNum))
                {
                    Console.WriteLine("No such menu item");
                    continue;
                }
                
                menuItems[itemNum].Command(user);
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
                List<User> notSubscriptionOwls = Users.Where(u => u is Owl 
                && !mouse.Subscriptions.Contains((Owl)u)).ToList();

                if (notSubscriptionOwls.Count == 0)
                {
                    Console.WriteLine("No owl for subscribe :(");
                    return;
                }
                Console.WriteLine("Enter the 'exit' to exit");
                Console.WriteLine("Enter the id of owl to subscribe: ");

                for (int i = 0; i < notSubscriptionOwls.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {notSubscriptionOwls[i].Name}");
                }

                string response = Console.ReadLine();
                int itemNum;

                if (response == "exit")
                {
                    MouseMenu(mouse);
                    return;
                }

                if (!int.TryParse(response, out itemNum) || itemNum < 1 || itemNum > notSubscriptionOwls.Count())
                {
                    Console.WriteLine("No such owl\n");
                    SelectNewOwlToSubscribe(mouse);
                    return;
                }
                itemNum--;

                //((Owl)owls[itemNum-1]).RegisterObserver(mouse);
                mouse.Subscribe(((Owl)notSubscriptionOwls[itemNum]));
                Console.WriteLine($"You're subscribed to '{((Owl)notSubscriptionOwls[itemNum]).Name}'");

                MouseMenu(mouse);
            }
        }

        private void WatchUnlikedPosts(User user)
        {
            if (user is Mouse mouse)
            {
                List<Post> unlikedPosts = mouse.Subscriptions
                .SelectMany(owl => owl.PublishedPosts)
                    .Where(post => !post.LikedMouses.Contains(mouse))
                .ToList<Post>();

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

                    if (!int.TryParse(response, out var itemNum) || itemNum < 1 || itemNum > unlikedPosts.Count())
                    {
                        Console.WriteLine("No such post :(");
                        Console.WriteLine("Enter the right command");
                        continue;
                    }

                    Console.WriteLine($"You're liked the post number {itemNum}");

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
