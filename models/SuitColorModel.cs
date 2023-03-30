using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace heroesBackend.models
{
	public class suitColorModel
	{
		public int Id { get; set; }

		[Required]
		public string Color { get; set; }

		public HeroModel Hero { get; set; }

	}
}

