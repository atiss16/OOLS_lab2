using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    public class RubberDuck : Duck
    {
        RubberDuck()
        {
            this.quackBehavior = new QuackSqueak();
            this.flyBehavior = new FlyNoWay();
        }
        public new void Display()
        {
            Console.WriteLine("RubberDuck display");
        }
    }
}
