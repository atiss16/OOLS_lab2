using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Quack
{
    /// <summary>
    /// поведение для уток, которые не могут крякать
    /// </summary>
    public class QuackNoWay : IQuackable
    {
        public void Quack()
        {
            Console.WriteLine("Silence");
        }
    }
}
