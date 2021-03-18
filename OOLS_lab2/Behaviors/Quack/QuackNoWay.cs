﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Quack
{
    public class QuackNoWay : IQuackable
    {
        public void Quack()
        {
            Console.WriteLine("Quack no way");
        }
    }
}