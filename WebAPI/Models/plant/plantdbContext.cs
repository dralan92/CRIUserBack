using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.models.plant
{
    public partial class plantdbContext : DbContext
    {
        public plantdbContext()
        {
        }

        public plantdbContext(DbContextOptions<plantdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Plants> Plants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=dralanserver.database.windows.net;Database=plantdb;Trusted_Connection=True;User Id=dralan;Password=Super*@123;Trusted_Connection=False;Encrypt=True;");
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plants>(entity =>
            {
                entity.HasKey(e => e.PlantId);

                entity.Property(e => e.LastWateredBy).HasMaxLength(50);

                entity.Property(e => e.PlantName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
