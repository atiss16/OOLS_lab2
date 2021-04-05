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
        public string Photo { get; set; }
        public Owl Sender { get; set; }
        public List<Mouse> LikedMouses { get; set; } = new List<Mouse>();
        //public List<Mouse> Recievers { get; set; } = new List<Mouse>();
        public DateTime PublicationTime { get; set; }

        public Post(Owl sender, string text, string geo, string photo)
        {
            Sender = sender;
            Text = text;
            Geo = geo;
            Photo = photo;
            LikedMouses = new List<Mouse>();
            PublicationTime = DateTime.Now;
        }
        public void Like(Mouse mouse)
        {
            if(!LikedMouses.Contains(mouse))
                LikedMouses.Add(mouse);
        }
    } 
}
