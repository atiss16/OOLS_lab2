using System;
using System.Collections.Generic;
using System.Linq;
using Owlgram.GameObjects;
using Owlgram.Observable;
using Owlgram.Decorator;


namespace Owlgram.GameRoles
{
    public class Mouse : User, IObserver
    {
        public DateTime CreateDate { get; private set; }
        public DateTime? DeadDate { get; private set; }
        public int LikedPostsCount;
        public List<Owl> Subscriptions { get; private set; } = new List<Owl>();
        private Notifier notifier;
        bool isLive;

        public Mouse()
        {
            
        }

        public Mouse(string name, string password, string geo = "", string photo = "")
        {
            Name = name;
            Photo = photo;
            Geo = geo;
            Password = password;

            CreateDate = DateTime.UtcNow;
            DeadDate = null;
            isLive = true;

            notifier = new BaseNotifierDecorator(notifier);
        }

        //метод смерти
        public void IsDead()
        {
            isLive = false;
            DeadDate = DateTime.UtcNow;
        }

        //получение времени жизни
        public int LifeTimeFromMinutes
        {
            get
            {
                if (DeadDate == null)
                    return (DateTime.UtcNow - CreateDate).Minutes;

                return (DeadDate.Value - CreateDate).Minutes;
            }
        }

        //метод подписки на сову
        public void Subscribe(Owl owl)
        {
            Subscriptions.Add(owl);
            owl.RegisterObserver(this);
        }

        //метод лайка поста
        public void Like(Post post)
        {
            post.Like(this);
            LikedPostsCount++;
        }

        void IObserver.Update(IPublisher publisher, Post post)
        {
            notifier.Send(post, this);
        }

        public void AddEmailNotification()
        {
            notifier = new EmailDecorator(notifier);
        }

        public void AddTelegramNotification()
        {
            notifier = new TelegramDecorator(notifier);
        }

        public void AddViberNotification()
        {
            notifier = new ViberDecorator(notifier);
        }

        public void AddWhatsAppNotification()
        {
            notifier = new WhatsAppDecorator(notifier);
        }

    }
}
