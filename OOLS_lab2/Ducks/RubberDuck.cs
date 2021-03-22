using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    /// <summary>
    /// Резиновая утка
    /// </summary>
    public class RubberDuck : Duck
    {
        protected override string Name => "RubberDuck";
        public RubberDuck()
        {
            this.quackBehavior = new QuackSqueak();
            this.flyBehavior = new FlyNoWay();
            this.swimBehavior = new SwimOnTheFlow();
        }
    }
}
