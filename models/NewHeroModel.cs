using System;
using heroesBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace heroesBackend.models
{
	public class NewHeroModel
	{
        [Required]
        public string Name { get; set; }

        [Required]
        public HeroAbility HeroAbility { get; set; }

      
        [Required]
        public IList<int> SuitColors { get; set; }

        [Required]
        public decimal StartingPower { get; set; }   
    
     }
}

