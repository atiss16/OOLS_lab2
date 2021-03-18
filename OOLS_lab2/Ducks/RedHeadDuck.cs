using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;


namespace DucksPond.Ducks
{
    /// <summary>
    /// Красноголовая утка
    /// </summary>
    public class RedHeadDuck : Duck
    {
        protected override string Name => "ReadHead";
        RedHeadDuck()
        {
            this.quackBehavior = new QuackSqueak();
            this.flyBehavior = new FlyWithWings();
        }
    }
}
