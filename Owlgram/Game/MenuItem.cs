using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameRoles;

namespace Owlgram.Game
{
    class MenuItem
    {
        public delegate void MenuItemCommand(User user = null);
        public MenuItemCommand Command;
        public string Description;

        public MenuItem(string descroption, MenuItemCommand command)
        {
            Description = descroption;
            Command = command;
        }
    }
}
