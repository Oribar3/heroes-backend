using System;

using System.ComponentModel.DataAnnotations;

namespace heroesBackend.models
{
	public class loginModel
    {
	    
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    
}
}

