using System;
using System.Collections.Generic;
using System.Linq;
using Owlgram.GameObjects;
using Owlgram.Observable;

namespace Owlgram.GameRoles
{
    public class Owl : IPublisher
    {
        public int Happiness;
        public int Satiety;
        public DateTime TimeOfLastPost;
        public List<IObserver> Observers;
        public Owl()
        {
            Happiness = 100;
            Satiety = 100;
            Observers = new List<IObserver>();
            TimeOfLastPost = DateTime.Now;
        }

        public void Uh(Post post)
        {
            //foreach (IObserver observer in Observers)
                //this.IPublisher.NotifyObserver(observer, post);
        }

        public void EatMouse(Mouse mouse)
        {
            mouse.IsDead();
            Satiety += 10;
        }

        public void NonPostingPunish()
        {
            if (Happiness <= 10)
                Happiness = 0;
            else
                Happiness -= 10;
        }

        public void NonEatingPunish()
        {
            if (Satiety <= 10)
                Satiety = 0;
            else
                Satiety -= 10;
        }

        void IPublisher.RegisterObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        void IPublisher.RemoveObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        void IPublisher.NotifyObserver(IObserver observer, Post post)
        {
            observer.Update(this, post);
        }
    }
}
