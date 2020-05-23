using ApiPostCreacionyConsumo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiPostCreacionyConsumo.Data
{
    public class AlumnosContext: DbContext
    {
        internal object Matricula;

        public AlumnosContext() : base("name=cadenaalumnos") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<AlumnosContext>(null);
        }

        public DbSet<Alumno> CARACTERISTICAS { get; set; }
       
    }
}