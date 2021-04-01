using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameObjects;

namespace Owlgram.Observable
{
    public interface IObserver
    {
        void Update(IPublisher publisher, Post post);
    }
}
