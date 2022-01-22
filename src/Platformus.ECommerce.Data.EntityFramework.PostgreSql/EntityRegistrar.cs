﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.PostgreSql
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Categories");
        }
      );

      modelBuilder.Entity<Product>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Url).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Code).HasMaxLength(64);
          etb.ToTable("Products");
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

      modelBuilder.Entity<Position>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Positions");
        }
      );

      modelBuilder.Entity<Cart>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Carts");
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
    }
  }
}