using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameObjects;

namespace Owlgram.GameRoles
{
    public class Owl
    {
        public int Happiness;
        public int Satiety;
        public DateTime TimeOfLastPost;
        public List<Mouse> Observers;
        public Owl()
        {
            Happiness = 100;
            Satiety = 100;
            Observers = new List<Mouse>();
            TimeOfLastPost = DateTime.Now;
        }

        public void Uh(Post post)
        {

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
    }
}
