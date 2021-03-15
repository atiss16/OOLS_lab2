using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Quack
{
    class QuackSqueak : IQuackable
    {
        public void Quack()
        {
            Console.WriteLine("Squeak!!!");
        }
    }
}
