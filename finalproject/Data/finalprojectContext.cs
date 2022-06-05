using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using finalproject.Models;

namespace finalproject.Data
{
    public partial class finalprojectContext : DbContext
    {
        public finalprojectContext()
        {
        }

        public finalprojectContext(DbContextOptions<finalprojectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Pop> Pops { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Country1)
                    .HasName("PRIMARY");

                entity.ToTable("countries");

                entity.Property(e => e.Country1)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.Iso3)
                    .HasMaxLength(10)
                    .HasColumnName("iso3");
            });

            modelBuilder.Entity<Pop>(entity =>
            {
                entity.HasKey(e => e.Year)
                    .HasName("PRIMARY");

                entity.ToTable("pop");

                entity.HasIndex(e => e.Country, "country");

                entity.Property(e => e.Year)
                    .ValueGeneratedNever()
                    .HasColumnName("year");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Pops)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pop_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
