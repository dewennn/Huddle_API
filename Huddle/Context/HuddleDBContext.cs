using System;
using Microsoft.EntityFrameworkCore;

namespace Huddle.Context
{
    public partial class HuddleDBContext : DbContext
    {
        // CONSTRUCTOR
        public HuddleDBContext() { }
        public HuddleDBContext(DbContextOptions<HuddleDBContext> options) : base(options) { }


        // DEFINE THE TABLES
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:HuddleDB");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary Key

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()"); // Assigns GUID in SQL Server

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(255);

                entity.Property(e => e.ProfilePictureUrl)
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .HasMaxLength(50);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100);

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255); // Matches SQL nvarchar(255)
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}