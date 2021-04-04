using System;
using System.Collections.Generic;
using System.Linq;
using Owlgram.GameObjects;
using Owlgram.Observable;


namespace Owlgram.GameRoles
{
    public class Mouse : IObserver
    {
        public DateTime CreateDate;
        public DateTime? DeadDate;
        bool isLive;

        public Mouse()
        {
            CreateDate = DateTime.UtcNow;
            DeadDate = null;
            isLive = true;
        }

        //метод смерти
        public void IsDead()
        {
            isLive = false;
            DeadDate = DateTime.UtcNow;
        }

        //метод получения времени жизни
        public int GetLifeTimeFromMinutes()
        {
            if (DeadDate == null)
                return (DateTime.UtcNow - CreateDate).Minutes;

            return (DeadDate.Value - CreateDate).Minutes;


        }

        //метод подписки на сову
        public void Subscribe(Owl owl)
        {
            owl.RegisterObserver(this);
        }

        //метод лайка поста
        public void Like(Post post)
        {
            post.Like(this);
        }

        void IObserver.Update(IPublisher publisher, Post post)
        {
            Random rand = new Random();
            if (rand.Next(2) == 0)
                return;
            else
                post.Like(this);
        }
    }
}
