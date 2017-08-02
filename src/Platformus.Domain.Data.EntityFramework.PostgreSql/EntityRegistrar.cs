// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.PostgreSql
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Class>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.PluralizedName).IsRequired().HasMaxLength(64);
          etb.ForNpgsqlToTable("Classes");
        }
      );

      modelBuilder.Entity<Tab>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForNpgsqlToTable("Tabs");
        }
      );

      modelBuilder.Entity<DataType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.StorageDataType).IsRequired().HasMaxLength(32);
          etb.Property(e => e.JavaScriptEditorClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForNpgsqlToTable("DataTypes");
        }
      );

      modelBuilder.Entity<DataTypeParameter>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.JavaScriptEditorClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForNpgsqlToTable("DataTypeParameters");
        }
      );

      modelBuilder.Entity<DataTypeParameterValue>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Value).IsRequired().HasMaxLength(1024);
          etb.ForNpgsqlToTable("DataTypeParameterValues");
        }
      );

      modelBuilder.Entity<Member>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForNpgsqlToTable("Members");
        }
      );

      modelBuilder.Entity<Object>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForNpgsqlToTable("Objects");
        }
      );

      modelBuilder.Entity<Property>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForNpgsqlToTable("Properties");
        }
      );

      modelBuilder.Entity<Relation>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.HasOne(e => e.Primary).WithMany(e => e.PrimaryRelations).HasForeignKey(e => e.PrimaryId);
          etb.HasOne(e => e.Foreign).WithMany(e => e.ForeignRelations).HasForeignKey(e => e.ForeignId);
          etb.ForNpgsqlToTable("Relations");
        }
      );

      modelBuilder.Entity<Microcontroller>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.UrlTemplate).HasMaxLength(128);
          etb.Property(e => e.SignInUrl).HasMaxLength(128);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ForNpgsqlToTable("Microcontrollers");
        }
      );

      modelBuilder.Entity<MicrocontrollerPermission>(etb =>
        {
          etb.HasKey(e => new { e.MicrocontrollerId, e.PermissionId });
          etb.ForNpgsqlToTable("MicrocontrollerPermissions");
        }
      );

      modelBuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ForNpgsqlToTable("DataSources");
        }
      );

      modelBuilder.Entity<SerializedObject>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.ObjectId });
          etb.Property(e => e.UrlPropertyStringValue).HasMaxLength(128);
          etb.ForNpgsqlToTable("SerializedObjects");
        }
      );
    }
  }
}