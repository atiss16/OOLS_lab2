using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owlgram.GameRoles
{
    public class User
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Geo { get; set; }
        public string Password { get; set; }
        public bool IsLive { get; set; }
        public User()
        {

        }

        public virtual void Dead()
        {
            IsLive = false;
        }
    }
}
