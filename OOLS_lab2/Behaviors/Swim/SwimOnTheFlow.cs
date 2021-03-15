using System;

namespace DucksPond.Behaviors.Swim
{
    public class SwimOnTheFlow : ISwimable
    {
        public void Swim()
        {
            Console.WriteLine("~~ I swim on the surface ~~");
        }
    }
}
