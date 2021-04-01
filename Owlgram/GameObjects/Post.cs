using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owlgram.GameRoles;

namespace Owlgram.GameObjects
{
    public class Post
    { 
        public string Text { get; set; }
        public string Geo { get; set; }
        public Owl Sender { get; set; }
        public List<Mouse> LikedMouses { get; set; }
        public List<Mouse> Recievers { get; set; }
        public DateTime PublicationTime { get; set; }

        public void Like(Mouse mouse)
        {
            if(!LikedMouses.Contains(mouse))
                LikedMouses.Add(mouse);
        }
    } 
}
