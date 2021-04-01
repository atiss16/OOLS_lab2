using Owlgram.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.Observable;

namespace Owlgram.Decorator
{
    public class BaseNotifierDecorator : Notifier
    {
        public Notifier Notifier;
        public BaseNotifierDecorator(Notifier notifier)
        {
            Notifier = notifier;
        }
        public override void Send(Post post, IObserver observer)
        {
            Console.WriteLine("Уведомления:");
        }
    }
}
