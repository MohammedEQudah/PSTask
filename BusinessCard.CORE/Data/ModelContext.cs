using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessCard.CORE.Data
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Businesscard> Businesscards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=c##Bcard;Password=Test321;Data Source=192.168.1.8:1521/xe");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("C##BCARD")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Businesscard>(entity =>
            {
                entity.ToTable("BUSINESSCARD");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEOFBIRTH");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
