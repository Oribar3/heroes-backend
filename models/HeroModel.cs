using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using heroesBackend.Entities;

namespace heroesBackend.models
{
    

    public class HeroModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public HeroAbility HeroAbility { get; set; }

        [Required]
        public DateTime ? TrainingStartDate { get; set; }

        [Required]
        public IList<suitColorModel> SuitColors { get; set; }

        [Required]
        [Precision(38,2)]//how many letters, how mant letters after point
        public decimal StartingPower { get; set; }

        [Precision(38, 2)]
        public decimal CurrentPower { get; set; }

        public Trainer ? Trainer { get; set; }

        public DateTime? LastTrainingDate { get; set; }

        public int? DailyTrainingCount { get; set; }
    }
}