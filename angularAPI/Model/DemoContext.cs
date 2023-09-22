using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace angularAPI.Model;

public partial class DemoContext : DbContext
{
    public DemoContext()
    {
    }

    public DemoContext(DbContextOptions<DemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Policyvehicle> Policyvehicles { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=VM-104;database=autopastraining;user id=apastraining;password=apastraining123.;sslmode=None", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PRIMARY");

            entity.ToTable("policy");

            entity.HasIndex(e => e.AppUserId, "Appuser2id_idx");

            entity.HasIndex(e => e.PolicyNumber, "PolicyNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.QuoteNumber, "QuoteNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PolicyId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Cgst)
                .HasPrecision(10, 2)
                .HasColumnName("CGST");
            entity.Property(e => e.EligibleForNcb)
                .HasDefaultValueSql("'0'")
                .HasColumnName("EligibleForNCB");
            entity.Property(e => e.Igst)
                .HasPrecision(10, 2)
                .HasColumnName("IGST");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.QuoteNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.ReceiptNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Sgst)
                .HasPrecision(10, 2)
                .HasColumnName("SGST");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.TotalFees).HasPrecision(10, 2);
            entity.Property(e => e.TotalPremium).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Policyvehicle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("policyvehicle");

            entity.HasIndex(e => e.PolicyId, "fk_polid");

            entity.HasIndex(e => e.VehicleId, "fk_vehicleid");

            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.VehicleId)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PRIMARY");

            entity.ToTable("vehicle");

            entity.HasIndex(e => e.BodyTypeId, "BodyId_idx");

            entity.HasIndex(e => e.BrandId, "BrandId_idx");

            entity.HasIndex(e => e.FuelTypeId, "FuelTypeId_idx");

            entity.HasIndex(e => e.ModelId, "ModelId_idx");

            entity.HasIndex(e => e.Rtoid, "RTOId_idx");

            entity.HasIndex(e => e.TransmissionTypeId, "TransmissionTypeId_idx");

            entity.HasIndex(e => e.VariantId, "VariantId_idx");

            entity.HasIndex(e => e.VehicleTypeid, "VehicleTypeId_idx");

            entity.HasIndex(e => e.VehicleId, "id_UNIQUE").IsUnique();

            entity.Property(e => e.VehicleId)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.ChasisNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.EngineNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.ExShowroomPrice).HasPrecision(10, 2);
            entity.Property(e => e.Idv)
                .HasPrecision(10, 2)
                .HasColumnName("IDV");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(45)
                .IsFixedLength();
            entity.Property(e => e.Rtoid).HasColumnName("RTOId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
