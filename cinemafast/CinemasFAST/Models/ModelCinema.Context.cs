﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemasFAST.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cinemadbEntities : DbContext
    {
        public cinemadbEntities()
            : base("name=cinemadbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<acteur> acteurs { get; set; }
        public virtual DbSet<cinema> cinemas { get; set; }
        public virtual DbSet<directeur> directeurs { get; set; }
        public virtual DbSet<direction> directions { get; set; }
        public virtual DbSet<estpresente> estpresentes { get; set; }
        public virtual DbSet<film> films { get; set; }
        public virtual DbSet<genre> genres { get; set; }
        public virtual DbSet<profil> profils { get; set; }
        public virtual DbSet<responsable> responsables { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<salle> salles { get; set; }
        public virtual DbSet<seance> seances { get; set; }
        public virtual DbSet<supplement> supplements { get; set; }
        public virtual DbSet<tarif> tarifs { get; set; }
    }
}
