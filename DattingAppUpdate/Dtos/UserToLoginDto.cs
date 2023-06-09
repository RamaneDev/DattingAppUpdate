﻿using System.ComponentModel.DataAnnotations;

namespace DattingAppUpdate.Dtos
{
    public class UserToLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 15 caracteres")]
        public string Password { get; set; }
    }
}
