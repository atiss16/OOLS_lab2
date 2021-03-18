using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksPond.Behaviors.Fly
{
    /// <summary>
    /// Интерфейс для летающих уток
    /// </summary>
    public interface IFlyable 
    {
        /// <summary>
        /// метод полета
        /// </summary>
        void Fly();
    }
}
