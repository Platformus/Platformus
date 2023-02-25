// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Data.EntityFramework.Sqlite;

public class EntityRegistrar : IEntityRegistrar
{
  public void RegisterEntities(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Class>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.HasOne(e => e.Parent).WithMany(e => e.Classes).HasForeignKey(e => e.ClassId);
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.Property(e => e.PluralizedName).IsRequired().HasMaxLength(64);
        etb.HasMany(e => e.Members).WithOne(e => e.Class).HasForeignKey(e => e.ClassId);
        etb.ToTable("Classes");
      }
    );

    modelBuilder.Entity<Tab>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.ToTable("Tabs");
      }
    );

    modelBuilder.Entity<DataType>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.StorageDataType).IsRequired().HasMaxLength(32);
        etb.Property(e => e.ParameterEditorCode).IsRequired().HasMaxLength(128);
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.ToTable("DataTypes");
      }
    );

    modelBuilder.Entity<DataTypeParameter>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.ParameterEditorCode).IsRequired().HasMaxLength(128);
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.ToTable("DataTypeParameters");
      }
    );

    modelBuilder.Entity<DataTypeParameterOption>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Value).IsRequired().HasMaxLength(1024);
        etb.ToTable("DataTypeParameterOptions");
      }
    );

    modelBuilder.Entity<Member>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.ToTable("Members");
      }
    );

    modelBuilder.Entity<Object>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.ToTable("Objects");
      }
    );

    modelBuilder.Entity<Property>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.ToTable("Properties");
      }
    );

    modelBuilder.Entity<Relation>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.HasOne(e => e.Primary).WithMany(e => e.PrimaryRelations).HasForeignKey(e => e.PrimaryId);
        etb.HasOne(e => e.Foreign).WithMany(e => e.ForeignRelations).HasForeignKey(e => e.ForeignId);
        etb.ToTable("Relations");
      }
    );

    modelBuilder.Entity<Menu>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.ToTable("Menus");
      }
    );

    modelBuilder.Entity<MenuItem>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Url).HasMaxLength(128);
        etb.ToTable("MenuItems");
      }
    );

    modelBuilder.Entity<Form>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.Property(e => e.FormHandlerCSharpClassName).IsRequired().HasMaxLength(128);
        etb.Property(e => e.FormHandlerParameters).HasMaxLength(1024);
        etb.ToTable("Forms");
      }
    );

    modelBuilder.Entity<FieldType>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.ToTable("FieldTypes");
      }
    );

    modelBuilder.Entity<Field>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.ToTable("Fields");
      }
    );

    modelBuilder.Entity<FieldOption>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.ToTable("FieldOptions");
      }
    );

    modelBuilder.Entity<CompletedForm>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.ToTable("CompletedForms");
      }
    );

    modelBuilder.Entity<CompletedField>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.ToTable("CompletedFields");
      }
    );

    modelBuilder.Entity<File>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.ToTable("Files");
      }
    );

    modelBuilder.Entity<Endpoint>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
        etb.Property(e => e.UrlTemplate).HasMaxLength(128);
        etb.Property(e => e.SignInUrl).HasMaxLength(128);
        etb.Property(e => e.RequestProcessorCSharpClassName).IsRequired().HasMaxLength(128);
        etb.Property(e => e.RequestProcessorParameters).HasMaxLength(1024);
        etb.ToTable("Endpoints");
      }
    );

    modelBuilder.Entity<EndpointPermission>(etb =>
      {
        etb.HasKey(e => new { e.EndpointId, e.PermissionId });
        etb.ToTable("EndpointPermissions");
      }
    );

    modelBuilder.Entity<DataSource>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.Property(e => e.Id).ValueGeneratedOnAdd();
        etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
        etb.Property(e => e.DataProviderCSharpClassName).IsRequired().HasMaxLength(128);
        etb.Property(e => e.DataProviderParameters).HasMaxLength(1024);
        etb.ToTable("DataSources");
      }
    );
  }
}