using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    /// <summary>
    /// Жареная утка
    /// </summary>
    public class RoastDuck : Duck
    {
        protected override string Name => "RoastDuck";
        public RoastDuck()
        {
            this.quackBehavior = new QuackNoWay();
            this.flyBehavior = new FlyNoWay();
            this.swimBehavior = new SwimSink();
        }
    }
}
