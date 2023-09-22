using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models;

public partial class RealEstateContext : DbContext
{
    public RealEstateContext()
    {
    }

    public RealEstateContext(DbContextOptions<RealEstateContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Estate> Estates { get; set; }

    public virtual DbSet<EstateType> EstateTypes { get; set; }

    public virtual DbSet<Realtor> Realtors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MQ4HUQ0\\SQLEXPRESS;Database=RealEstate;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3214EC07D137CCF1");

            entity.ToTable("Address");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Street).HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC072BB49E3B");

            entity.ToTable("Client");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(100);
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__District__3214EC074BF60AFF");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Estate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estate__3214EC0768C68B31");

            entity.ToTable("Estate");

            entity.HasOne(d => d.Address).WithMany(p => p.Estates)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Estate__AddressI__76969D2E");

            entity.HasOne(d => d.Type).WithMany(p => p.Estates)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Estate__TypeId__778AC167");
        });

        modelBuilder.Entity<EstateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstateTy__3214EC07B8D9C6B8");

            entity.ToTable("EstateType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Realtor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Realtor__3214EC071A16169C");

            entity.ToTable("Realtor");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
