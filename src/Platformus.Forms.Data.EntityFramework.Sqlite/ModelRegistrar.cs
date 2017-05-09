// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<SerializedForm>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.FormId });
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("SerializedForms");
        }
      );

      modelBuilder.Entity<Form>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Email).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("Forms");
        }
      );

      modelBuilder.Entity<FieldType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqliteToTable("FieldTypes");
        }
      );

      modelBuilder.Entity<Field>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForSqliteToTable("Fields");
        }
      );

      modelBuilder.Entity<FieldOption>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForSqliteToTable("FieldOptions");
        }
      );

      modelBuilder.Entity<CompletedForm>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForSqliteToTable("CompletedForms");
        }
      );

      modelBuilder.Entity<CompletedField>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ForSqliteToTable("CompletedFields");
        }
      );
    }
  }
}