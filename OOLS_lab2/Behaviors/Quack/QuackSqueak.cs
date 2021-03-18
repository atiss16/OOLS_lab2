using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Quack
{
    /// <summary>
    /// поведение для уток, которые пищат
    /// </summary>
    public class QuackSqueak : IQuackable
    {
        public void Quack() 
        {
            Console.WriteLine("Squeak!!!");
        }
    }
}
