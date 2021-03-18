using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Quack
{
    /// <summary>
    /// Интерфейс кряканья
    /// </summary>
    public interface IQuackable 
    {
        /// <summary>
        /// метод кряканья
        /// </summary>
        void Quack();
    }
}
