using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    public abstract class Duck
    {
        public IFlyable flyBehavior;
        public IQuackable quackBehavior;
        protected abstract string Name { get; }
        public void PerformQuack() 
        {
            quackBehavior.Quack();
        }
        public void PerformFly()
        {
            flyBehavior.Fly();
        }

        public void Display()
        {
            Console.WriteLine($"{Name} display");
        }
    }
}
