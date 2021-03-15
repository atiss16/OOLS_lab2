using System;

namespace DucksPond.Behaviors.Swim
{
    /// <summary>
    /// Определяет неумение плавать
    /// </summary>
    public class SwimSink : ISwimable
    {
        public void Swim()
        {
            Console.WriteLine("↓↓ I am swimming in the depths and what will you do to me? ↓↓");
        }
    }
}
