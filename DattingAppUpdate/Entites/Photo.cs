using System;

namespace DattingAppUpdate.Entites
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public string Description { get; set; } // TODO: see if we can remove this property
        public DateTime DateAdded { get; set; } // TODO: see if we can remove this property
        public bool IsMain { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}