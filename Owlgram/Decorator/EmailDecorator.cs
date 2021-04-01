using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameObjects;
using Owlgram.Observable;

namespace Owlgram.Decorator
{
    public class EmailDecorator : BaseNotifierDecorator
    {
        public EmailDecorator(Notifier notifier) : base(notifier)
        {

        }

        public override void Send(Post post, IObserver observer)
        {
            this.Notifier.Send(post, observer);
            Console.WriteLine("Email уведомление:");
        }
    }
}
