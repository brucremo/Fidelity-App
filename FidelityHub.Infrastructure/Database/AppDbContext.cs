using FidelityHub.Database.Entities.AppSchema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Schema = "app";
        }

        private string Schema { get; }

        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionSale> PromotionSales { get; set; }
        public DbSet<PromotionType> PromotionTypes { get; set; }
        public DbSet<VendorUnit> VendorUnits { get; set; }
        public DbSet<PromotionCustomer> PromotionCustomers { get; set; }
        public DbSet<PromotionReward> PromotionRewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Promotion>()
                .ToTable("Promotion", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<PromotionSale>()
                .ToTable("PromotionSale", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<PromotionType>()
                .ToTable("PromotionType", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<VendorUnit>()
                .ToTable("VendorUnit", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<PromotionCustomer>()
                .ToTable("PromotionCustomer", this.Schema)
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<PromotionReward>()
                .ToTable("PromotionReward", this.Schema)
                .HasKey(c => new { c.Id });
        }
    }
}
