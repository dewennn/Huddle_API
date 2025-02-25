using System;
using System.Collections.Generic;
using Huddle.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Huddle.Context
{
    public partial class HuddleDBContext : DbContext
    {
        public HuddleDBContext() { }
        public HuddleDBContext(DbContextOptions<HuddleDBContext> options) : base(options) { }

        // TABLES
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Friendship> Friendships { get; set; } = null!; // Add Friendships table

        // CONNECT TO DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DEWEN;Database=HuddleDB;Trusted_Connection=True;");
            }
        }

        // MODEL MAPPING
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MESSAGE
            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ChannelTarget)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChannelTargetId)
                    .HasConstraintName("FK_ChannelTargetId");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SenderId");

                entity.HasOne(d => d.UserTarget)
                    .WithMany(p => p.MessageUserTargets)
                    .HasForeignKey(d => d.UserTargetId)
                    .HasConstraintName("FK_UserTargetId");
            });

            // SERVER
            modelBuilder.Entity<Server>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ProfilePictureUrl).HasMaxLength(255);
            });

            // USERS
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534F2F8D761")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AboutMe).HasMaxLength(255);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DateOfBirth).HasMaxLength(255);

                entity.Property(e => e.DisplayName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.OnlineStatus).HasMaxLength(50);

                entity.Property(e => e.PasswordHashed).HasMaxLength(255);

                entity.Property(e => e.ProfilePictureUrl).HasMaxLength(255);

                entity.Property(e => e.UserStatus).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            // FRIENDSHIP TABLE
            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.HasKey(f => new { f.UserOneId, f.UserTwoId }); // Composite Primary Key

                entity.HasOne(f => f.UserOne)
                    .WithMany()
                    .HasForeignKey(f => f.UserOneId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_User_One");

                entity.HasOne(f => f.UserTwo)
                    .WithMany()
                    .HasForeignKey(f => f.UserTwoId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_User_Two");
            });

            modelBuilder.Entity<Friendship>().ToTable("friends");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
