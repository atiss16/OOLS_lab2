using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Owlgram.GameObjects;
using Owlgram.Observable;

namespace Owlgram.GameRoles
{
    public class Owl : User, IPublisher
    {
        public int Happiness { get; private set; }
        public int Satiety { get; private set; }
        public int EatenMiceCount { get; private set; }
        public DateTime TimeOfLastPost { get; private set; }
        public List<Mouse> Observers { get; private set; }
        public List<Post> PublishedPosts { get; private set; }
        
        public Owl()
        {

        }

        public Owl(string name, string password, string geo ="", string photo="")
        {
            Name = name;
            Photo = photo;
            Geo = geo;
            Password = password;
            Happiness = 100;
            Satiety = 100;
            Observers = new List<Mouse>();
            TimeOfLastPost = DateTime.Now;
            EatenMiceCount = 0;
            IsLive = true;
            Observers = new List<Mouse>();
            PublishedPosts = new List<Post>();

        }
        
        public void Uh(Post post)
        {
            Happiness += 10;
            PublishedPosts.Add(post);
            TimeOfLastPost = DateTime.Now;
            foreach (IObserver observer in Observers)
                this.NotifyObserver(observer, post);
        }
        /// <summary>
        /// Метод охоты.
        /// </summary>
        /// <param name="numberUnlikedPostsToEatMouse">Необходимое количество постов, 
        /// которое не пролайкала мышь, чтобы ее съесть</param>
        /// <returns>Список съеденных мышей</returns>
        public List<Mouse> Hunt(int numberUnlikedPostsToEatMouse)
        {
            List<Mouse> subscribers = this.Observers;
            List<Mouse> pretendentsToEat = new List<Mouse>();
            Dictionary<Mouse, int> MouseLikedPostsCount = new Dictionary<Mouse, int>();

            foreach(Mouse mouse in subscribers)
            {
                MouseLikedPostsCount.Add(mouse, 0);
            }

            foreach (Post post in this.PublishedPosts)
            {
                foreach (Mouse mouse in post.LikedMouses)
                {
                    MouseLikedPostsCount[mouse]++;
                }
            }

            foreach (Mouse mouse in MouseLikedPostsCount.Keys)
            {
                int mouseUnlikedPostsCount = this.PublishedPosts.Count - MouseLikedPostsCount[mouse];
                if (mouseUnlikedPostsCount >= numberUnlikedPostsToEatMouse)
                {
                    this.EatMouse(mouse);
                    pretendentsToEat.Add(mouse);
                }
            }

            return pretendentsToEat;

            //List<Mouse> pretendentsToEat = this.Observers;

            //for (int i = 0; i < numberUnlikedPostsToEatMouse; i++)
            //{
            //    foreach (Post post in this.PublishedPosts)
            //    {
            //        pretendentsToEat.Except(post.LikedMouses);
            //    }
            //}
            //foreach (Mouse mouse in pretendentsToEat)
            //{
            //    this.EatMouse(mouse);
            //}
            //return pretendentsToEat;
        }

        public void EatMouse(Mouse mouse)
        {
            mouse.Dead();
            Satiety += 10;
            EatenMiceCount++;
        }

        public void NonPostingPunish()
        {
            if (Happiness <= 10)
            {
                Happiness = 0;
                Dead();
            }
            else
                Happiness -= 10;
        }

        public void NonEatingPunish()
        {
            if (Satiety <= 10)
            {
                Satiety = 0;
                Dead();
            }
            else
                Satiety -= 10;
        }

        public void RegisterObserver(IObserver observer)
        {
            Observers.Add((Mouse)observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            Observers.Remove((Mouse)observer);
        }

        public void NotifyObserver(IObserver observer, Post post)
        {
            observer.Update(this, post);
        }
    }
}
