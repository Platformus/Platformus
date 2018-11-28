// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.Sqlite
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Catalog>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Url).IsRequired().HasMaxLength(128);
          etb.ToTable("Catalogs");
        }
      );

      modelBuilder.Entity<Category>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Categories");
        }
      );

      modelBuilder.Entity<Feature>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("Features");
        }
      );

      modelBuilder.Entity<Attribute>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Attributes");
        }
      );

      modelBuilder.Entity<Product>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Url).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("Products");
        }
      );

      modelBuilder.Entity<ProductAttribute>(etb =>
        {
          etb.HasKey(e => new { e.ProductId, e.AttributeId });
          etb.ToTable("ProductAttributes");
        }
      );

      modelBuilder.Entity<Photo>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Filename).IsRequired().HasMaxLength(64);
          etb.ToTable("Photos");
        }
      );

      modelBuilder.Entity<Cart>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.ClientSideId).IsRequired().HasMaxLength(64);
          etb.ToTable("Carts");
        }
      );

      modelBuilder.Entity<OrderState>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("OrderStates");
        }
      );

      modelBuilder.Entity<PaymentMethod>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("PaymentMethods");
        }
      );

      modelBuilder.Entity<DeliveryMethod>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("DeliveryMethods");
        }
      );

      modelBuilder.Entity<Order>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.CustomerFirstName).IsRequired().HasMaxLength(64);
          etb.Property(e => e.CustomerLastName).HasMaxLength(64);
          etb.Property(e => e.CustomerPhone).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CustomerEmail).HasMaxLength(64);
          etb.Property(e => e.CustomerAddress).HasMaxLength(128);
          etb.Property(e => e.Note).HasMaxLength(1024);
          etb.ToTable("Orders");
        }
      );

      modelBuilder.Entity<Position>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Positions");
        }
      );

      modelBuilder.Entity<SerializedProduct>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.ProductId });
          etb.Property(e => e.Url).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Title).HasMaxLength(128);
          etb.Property(e => e.MetaDescription).HasMaxLength(512);
          etb.Property(e => e.MetaKeywords).HasMaxLength(256);
          etb.ToTable("SerializedProducts");
        }
      );

      modelBuilder.Entity<SerializedAttribute>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.AttributeId });
          etb.Property(e => e.Value).IsRequired().HasMaxLength(64);
          etb.ToTable("SerializedAttributes");
        }
      );
    }
  }
}