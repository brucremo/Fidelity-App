using FidelityHub.Database.Entities.UsrSchema;
using FidelityHub.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityHub.Infrastructure.Database
{
    public class DboDbContext : IdentityDbContext<AspNetUser>
    {
        public DboDbContext(DbContextOptions<DboDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AspNetUserTokens> CustomUserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .ToTable("AspNetUsers", "dbo")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<RefreshToken>()
                .ToTable("RefreshTokens", "dbo")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<AspNetUserTokens>()
                .ToTable("AspNetUserTokens", "dbo")
                .HasKey(c => new { c.UserId, c.Name });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.AddAuditInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            this.AddAuditInfo();
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
