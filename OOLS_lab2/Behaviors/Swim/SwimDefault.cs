using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Swim
{
    public class SwimDefault : ISwimable
    {
        public void Swim()
        {
            Console.WriteLine("~~ I swim like an ordinary duck ~~");
        }
    }
}
