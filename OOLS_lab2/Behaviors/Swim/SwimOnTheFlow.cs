using System;

namespace DucksPond.Behaviors.Swim
{
    /// <summary>
    /// Определяет поведение для уток, которые плавают на поверхности
    /// </summary>
    public class SwimOnTheFlow : ISwimable
    {
        public void Swim() 
        {
            Console.WriteLine("~~ I swim on the surface ~~");
        }
    }
}
