using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    /// <summary>
    /// Утка кряква
    /// </summary>
    public class MallardDuck : Duck
    {
        protected override string Name => "MallardDuck";
        public MallardDuck()
        {
            this.quackBehavior = new QuackDefault();
            this.flyBehavior = new FlyWithWings();
            this.swimBehavior = new SwimDefault();
        }
    }
}
