using FidelityHub.Database.Entities.RefSchema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Infrastructure.Database
{
    public class RefDbContext : DbContext
    {
        public RefDbContext(DbContextOptions<RefDbContext> options) : base(options)
        {
            this.Schema = "ref";
        }

        private string Schema { get; }

        public DbSet<SentContactRequests> ContactRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SentContactRequests>()
                .ToTable("SentContactRequests", this.Schema)
                .HasKey(c => new { c.Id });
        }
    }
}
