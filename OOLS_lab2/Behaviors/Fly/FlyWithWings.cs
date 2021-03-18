using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Fly
{
    /// <summary>
    /// поведение для уток, которые умеют летать
    /// </summary>
    public class FlyWithWings : IFlyable 
    {
        public void Fly()
        {
            Console.WriteLine("Fly no way");
        }
    }
}
