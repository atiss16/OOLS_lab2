﻿using System;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;


namespace DucksPond.Ducks
{
    public class ReadHeadDuck : Duck
    {
        ReadHeadDuck()
        {
            this.quackBehavior = new QuackSqueak();
            this.flyBehavior = new FlyWithWings();
        }
        public new void Display()
        {
            Console.WriteLine("ReadHeadDuck display");
        }
    }
}