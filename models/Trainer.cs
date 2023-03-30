using System;
using Microsoft.AspNetCore.Identity;


namespace heroesBackend.models
{
    public class Trainer : IdentityUser
    {
        public string Name { get; set; }

        public IList<HeroModel>? Heroes { get; set; }
    }
}

