﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Atestados.Datos.Modelo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AtestadosEntities : DbContext
    {
        public AtestadosEntities()
            : base("name=AtestadosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Archivo> Archivoes { get; set; }
        public virtual DbSet<Atestado> Atestadoes { get; set; }
        public virtual DbSet<AtestadoXPersona> AtestadoXPersonas { get; set; }
        public virtual DbSet<DominioIdioma> DominioIdiomas { get; set; }
        public virtual DbSet<Fecha> Fechas { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<InfoEditorial> InfoEditorials { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Rubro> Rubroes { get; set; }
        public virtual DbSet<TipoRubro> TipoRubroes { get; set; }
    }
}