using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    /// <summary>
    /// Деревянная утка
    /// </summary>
    public class WoodenDuck : Duck
    {
        protected override string Name => "WoodenDuck";
        WoodenDuck()
        {
            this.quackBehavior = new QuackNoWay(); 
            this.flyBehavior = new FlyNoWay();
            this.swimBehavior = new SwimOnTheFlow();
        }
    }
}
