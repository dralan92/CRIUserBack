using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models.Cri
{
    public partial class criTestQnsContext : DbContext
    {
        public criTestQnsContext()
        {
        }

        public criTestQnsContext(DbContextOptions<criTestQnsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CriQn> CriQn { get; set; }
        public virtual DbSet<Tier> Tier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-63SSQ6I\\SQLEXPRESS;Database=criTestQns;Trusted_Connection=True;MultipleActiveResultSets=True;User Id=sa;Password=pioneer@123;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CriQn>(entity =>
            {
                entity.HasKey(e => e.QnId);

                entity.HasIndex(e => e.CountryFk);

                entity.HasIndex(e => e.TierFk);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Opt1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Opt2)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Opt3).HasMaxLength(50);

                entity.Property(e => e.Opt4).HasMaxLength(50);

                entity.Property(e => e.Qn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CountryFkNavigation)
                    .WithMany(p => p.CriQn)
                    .HasForeignKey(d => d.CountryFk);

                entity.HasOne(d => d.TierFkNavigation)
                    .WithMany(p => p.CriQn)
                    .HasForeignKey(d => d.TierFk);
            });

            modelBuilder.Entity<Tier>(entity =>
            {
                entity.Property(e => e.TierName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
