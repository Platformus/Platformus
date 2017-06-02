// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Class>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.PluralizedName).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("Classes");
        }
      );

      modelBuilder.Entity<Tab>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("Tabs");
        }
      );

      modelBuilder.Entity<DataType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.StorageDataType).IsRequired().HasMaxLength(32);
          etb.Property(e => e.JavaScriptEditorClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("DataTypes");
        }
      );

      modelBuilder.Entity<DataTypeParameter>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.JavaScriptEditorClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("DataTypeParameters");
        }
      );

      modelBuilder.Entity<DataTypeParameterValue>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Value).IsRequired().HasMaxLength(1024);
          etb.ForSqliteToTable("DataTypeParameterValues");
        }
      );

      modelBuilder.Entity<Member>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("Members");
        }
      );

      modelBuilder.Entity<Object>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForSqliteToTable("Objects");
        }
      );

      modelBuilder.Entity<Property>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForSqliteToTable("Properties");
        }
      );

      modelBuilder.Entity<Relation>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.HasOne(e => e.Primary).WithMany(e => e.PrimaryRelations).HasForeignKey(e => e.PrimaryId);
          etb.HasOne(e => e.Foreign).WithMany(e => e.ForeignRelations).HasForeignKey(e => e.ForeignId);
          etb.ForSqliteToTable("Relations");
        }
      );

      modelBuilder.Entity<Microcontroller>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.UrlTemplate).HasMaxLength(128);
          etb.Property(e => e.ViewName).IsRequired().HasMaxLength(64);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.ForSqliteToTable("Microcontrollers");
        }
      );

      modelBuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ForSqliteToTable("DataSources");
        }
      );

      modelBuilder.Entity<SerializedObject>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.ObjectId });
          etb.Property(e => e.UrlPropertyStringValue).HasMaxLength(128);
          etb.ForSqliteToTable("SerializedObjects");
        }
      );
    }
  }
}