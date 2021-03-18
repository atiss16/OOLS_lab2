using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksPond.Behaviors.Fly;
using DucksPond.Behaviors.Quack;
using DucksPond.Behaviors.Swim;

namespace DucksPond.Ducks
{
    public abstract class Duck
    {
        public IFlyable flyBehavior; //наследуются всеми субклассами Duck
        public IQuackable quackBehavior;
        public void PerformQuack() 
        {
            quackBehavior.Quack();  // делегирование операции классам поведения
        }
        public void PerformFly()
        {
            flyBehavior.Fly();
        }

        //нада решить что делац
        public void Display()
        {

        }
    }
}
