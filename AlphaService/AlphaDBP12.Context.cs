﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlphaService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AlphaDBP12Entities1 : DbContext
    {
        public AlphaDBP12Entities1()
            : base("name=AlphaDBP12Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Platoons> Platoons { get; set; }
        public virtual DbSet<Recruits> Recruits { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
    }
}