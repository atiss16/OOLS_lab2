using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksPond.Behaviors;
using DucksPond.Ducks;
using DucksPond.Fixtures;

namespace DucksPond
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Duck> Ducks = new List<Duck>();
            Ducks.Add(new MallardDuck());
            Ducks.Add(new RubberDuck());
            Ducks.Add(new RedHeadDuck());
            Ducks.Add(new RoastDuck());
            Ducks.Add(new RubberDuck());

            foreach(Duck duck in Ducks)
            {
                duck.Display();
                duck.PerformFly();
                duck.PerformQuack();
                duck.PerformSwim();
                Console.WriteLine();
            }

            DuckDecoy duckDecoy = new DuckDecoy();
            duckDecoy.PerformQuack();
            
        }
    }
}
