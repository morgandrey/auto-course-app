using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SellAutoApp.Models;

namespace SellAutoApp.DataAccess;

public partial class SellCarDbContext : DbContext
{
    public SellCarDbContext()
    {
    }

    public SellCarDbContext(DbContextOptions<SellCarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<CarModelOrder> CarModelOrders { get; set; }

    public virtual DbSet<CarModelPrice> CarModelPrices { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Transmission> Transmissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SellCarDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasKey(e => e.CarModelId).HasName("Car_Model_pk");

            entity.ToTable("Car_Model");

            entity.Property(e => e.CarModelId).HasColumnName("car_model_id");
            entity.Property(e => e.CarModelName).HasColumnName("car_model_name");
            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.TransmissionId).HasColumnName("transmission_id");

            entity.HasOne(d => d.Color).WithMany(p => p.CarModels)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Model_Color_null_fk");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.CarModels)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Model_Manufacturer_null_fk");

            entity.HasOne(d => d.Transmission).WithMany(p => p.CarModels)
                .HasForeignKey(d => d.TransmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Model_Transmission_null_fk");
        });

        modelBuilder.Entity<CarModelOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Car_Model_Order_pk");

            entity.ToTable("Car_Model_Order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarModelId).HasColumnName("car_model_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.CarModel).WithMany(p => p.CarModelOrders)
                .HasForeignKey(d => d.CarModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Model_Order_Car_Model_null_fk");

            entity.HasOne(d => d.Order).WithMany(p => p.CarModelOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Model_Order_Order_null_fk");
        });

        modelBuilder.Entity<CarModelPrice>(entity =>
        {
            entity.HasKey(e => e.CarModelPriceId).HasName("Car_Model_Price_pk");

            entity.ToTable("Car_Model_Price");

            entity.Property(e => e.CarModelPriceId).HasColumnName("car_model_price_id");
            entity.Property(e => e.CarModelId).HasColumnName("car_model_id");
            entity.Property(e => e.CarModelPrice1).HasColumnName("car_model_price");
            entity.Property(e => e.CarModelPriceUpdate)
                .HasColumnType("datetime")
                .HasColumnName("car_model_price_update");

            entity.HasOne(d => d.CarModel).WithMany(p => p.CarModelPrices)
                .HasForeignKey(d => d.CarModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Model_Price_Car_Model_null_fk");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("Color_pk");

            entity.ToTable("Color");

            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.ColorName).HasColumnName("color_name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("Manufacturer_pk");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.ManufacturerName).HasColumnName("manufacturer_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order_pk");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_User_null_fk");
        });

        modelBuilder.Entity<Transmission>(entity =>
        {
            entity.HasKey(e => e.TransmissionId).HasName("Transmission_pk");

            entity.ToTable("Transmission");

            entity.Property(e => e.TransmissionId)
                .ValueGeneratedNever()
                .HasColumnName("transmission_id");
            entity.Property(e => e.TransmissionName).HasColumnName("transmission_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserHash).HasColumnName("user_hash");
            entity.Property(e => e.UserLogin).HasColumnName("user_login");
            entity.Property(e => e.UserName).HasColumnName("user_name");
            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.UserSurname).HasColumnName("user_surname");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_User_Role_null_fk");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("User_Role_pk");

            entity.ToTable("User_Role");

            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.UserRoleName).HasColumnName("user_role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
