using System;
using System.ComponentModel.DataAnnotations;

namespace heroesBackend.models
{
	public class signUpModel
	{
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

