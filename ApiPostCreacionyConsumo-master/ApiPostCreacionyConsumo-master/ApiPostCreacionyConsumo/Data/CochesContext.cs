using ApiPostCreacionyConsumo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiPostCreacionyConsumo.Data
{
    public class CochesContext: DbContext
    {
        public CochesContext() : base("name=cadenacoches") { }
        public DbSet<Coche> Coches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CochesContext>(null);
        }
    }
}