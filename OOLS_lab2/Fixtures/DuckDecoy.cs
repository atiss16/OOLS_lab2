using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Fixtures
{
    /// <summary>
    /// Утиный манок - приспособление для имитации голоса утки
    /// </summary>
    public class DuckDecoy
    {
        public IQuackable quackBehavior;
        public DuckDecoy()
        {
            this.quackBehavior = new QuackDefault();
        }
        public void PerformQuack()
        {
            quackBehavior.Quack();
        }
    }
}