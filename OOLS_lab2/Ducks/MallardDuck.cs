using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    public class MallardDuck : Duck
    {
        MallardDuck()
        {
            this.quackBehavior = new QuackDefault();
            this.flyBehavior = new FlyWithWings();
        }
        public new void Display()
        {
            Console.WriteLine("MallardDuck display");
        }
    }
}
