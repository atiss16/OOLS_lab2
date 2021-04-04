using System;
using System.Collections.Generic;
using System.Linq;
using Owlgram.GameObjects;
using Owlgram.Observable;


namespace Owlgram.GameRoles
{
    public class Mouse : IObserver
    {
        public int LifeTime;
        public bool IsLive;
        public List<IPublisher> Observers;

        public Mouse()
        {
            LifeTime = 0;
            IsLive = true;
        }

        //метод смерти
        public void IsDead()
        {
            IsLive = false;
        }

        //метод увеличения времени жизни на 1
        public void UpLifeTime()
        {
            LifeTime++;
        }

        //метод подписки на сову
        public void Subscribe(Owl owl)
        {

        }

        //метод лайка поста
        public void Like(Post post)
        {

        }

        void IObserver.Update(IPublisher publisher, Post post)
        {

        }
    }
}
