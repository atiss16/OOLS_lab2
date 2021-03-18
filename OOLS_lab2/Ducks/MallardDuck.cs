using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    public class MallardDuck : Duck
    {
        protected override string Name => "MallardDuck";
        MallardDuck()
        {
            this.quackBehavior = new QuackDefault();
            this.flyBehavior = new FlyWithWings();
        }
    }
}
