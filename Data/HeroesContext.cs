using System;
using heroesBackend.models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace heroesBackend.Data
{
    public class HeroesContext : IdentityDbContext<Trainer>
    {

        public HeroesContext(DbContextOptions<HeroesContext> options)
            : base(options)
        {
        }

        public DbSet<HeroModel> Heroes { get; set; }
        public DbSet<suitColorModel> SuitColors { get; set; }

    }
}

