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

        public virtual DbSet<Countries> Countries { get; set; } = null!;
        public virtual DbSet<Pop> Pops { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.country)
                    .HasName("PRIMARY");

                entity.ToTable("countries");

                entity.Property(e => e.country)
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
                entity.ToTable("pop");

                entity.HasIndex(e => e.Country, "country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.value).HasColumnName("value");

                entity.Property(e => e.year).HasColumnName("year");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.populationCounts)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pop_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
