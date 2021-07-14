using FidelityHub.Application.Models.Registration;
using FidelityHub.Database.Entities.UsrSchema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Infrastructure.Database
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            this.Schema = "usr";
        }

        private string Schema { get; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<VendorRelationship> VendorRelationship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>()
                .ToTable("Vendor", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<UserType>()
                .ToTable("UserTypes", this.Schema)
                .HasKey(c => new { c.UserTypeId });

            modelBuilder.Entity<Subscription>()
                .ToTable("Subscription", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<Address>()
                .ToTable("Address", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<Customer>()
                .ToTable("Customer", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<VendorRelationship>()
                .ToTable("VendorRelationship", this.Schema)
                .HasKey(c => new { c.UserId, c.VendorId, c.VendorUnitId });
        }
    }
}
