﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WAMekaWash.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDMekawashEntities : DbContext
    {
        public BDMekawashEntities()
            : base("name=BDMekawashEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Local> Local { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
