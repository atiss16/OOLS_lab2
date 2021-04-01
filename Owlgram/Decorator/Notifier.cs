using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameObjects;
using Owlgram.Observable;

namespace Owlgram.Decorator
{
    public abstract class Notifier
    {
        public abstract void Send(Post post, IObserver observer);

    }
}
