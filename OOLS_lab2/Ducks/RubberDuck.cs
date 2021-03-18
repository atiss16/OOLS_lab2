﻿using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    public class RubberDuck : Duck
    {
        protected override string Name => "RubberDuck";
        RubberDuck()
        {
            this.quackBehavior = new QuackSqueak();
            this.flyBehavior = new FlyNoWay();
        }
    }
}
