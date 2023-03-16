using System.Collections.Generic;
using System;

namespace DattingAppUpdate.Dtos
{
    public class UserToReturn
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnowsAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoToReturn> Photos { get; set; }
    }
}
