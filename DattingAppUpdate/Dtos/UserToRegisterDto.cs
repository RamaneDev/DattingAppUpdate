using System;
using System.ComponentModel.DataAnnotations;

namespace DattingAppUpdate.Dtos
{
    public class UserToRegisterDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string KnowsAs { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string Country { get; set; }
        
        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 15 caracteres")]
        public string Password { get; set; }
    }
}
