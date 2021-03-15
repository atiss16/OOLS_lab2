using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Fly
{
    public class FlyNoWay : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("Fly no way");
        }
    }
}
