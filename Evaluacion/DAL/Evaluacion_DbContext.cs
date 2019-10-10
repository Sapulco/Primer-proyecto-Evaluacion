using Evaluacion.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Evaluacion.DAL
{
    public class Evaluacion_DbContext : DbContext
    {
        public Evaluacion_DbContext() : base ("csEvaluacion")
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<PerfilModulo> PerfilModulo { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<PerfilDepartamento> PerfilDepartamento { get; set; }
        public DbSet<Examen> Examen { get; set; }
        public DbSet<Pregunta> Pregunta { get; set; }
        public DbSet<TipoPregunta> TipoPregunta { get; set; }
        public DbSet<Opcion> Opcion { get; set; }
        public DbSet<Resultado> Resultado { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
        }
    }
}