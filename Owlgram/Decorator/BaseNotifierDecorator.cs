using Owlgram.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.Observable;
using Owlgram.GameRoles;

namespace Owlgram.Decorator
{
    public class BaseNotifierDecorator : Notifier
    {
        public Notifier Notifier;
        public BaseNotifierDecorator(Notifier notifier)
        {
            Notifier = notifier;
        }
        public override void Send(Post post, Mouse mouse)
        {
            Console.WriteLine(mouse + " уведомлена");
        }
    }
}
