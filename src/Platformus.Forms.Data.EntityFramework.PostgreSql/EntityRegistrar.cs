// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.PostgreSql
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Form>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
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

      modelBuilder.Entity<SerializedForm>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.FormId });
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("SerializedForms");
        }
      );
    }
  }
}