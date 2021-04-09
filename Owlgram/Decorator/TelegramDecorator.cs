using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameObjects;
using Owlgram.Observable;
using Owlgram.GameRoles;

namespace Owlgram.Decorator
{
    public class TelegramDecorator : BaseNotifierDecorator
    {
        public TelegramDecorator(Notifier notifier) : base(notifier)
        {

        }

        public override void Send(Post post, Mouse mouse)
        {
            this.Notifier.Send(post, mouse);
            Console.WriteLine($"'{mouse.Name}' уведомлена по Telegram");
        }
    }
}
