// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CachedObject>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.ObjectId });
          etb.Property(e => e.ViewName).HasMaxLength(32);
          etb.Property(e => e.Url).HasMaxLength(128);
          etb.ForSqlServerToTable("CachedObjects");
        }
      );

      modelBuilder.Entity<Class>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.PluralizedName).IsRequired().HasMaxLength(64);
          etb.Property(e => e.DefaultViewName).HasMaxLength(32);
          etb.ForSqlServerToTable("Classes");
        }
      );

      modelBuilder.Entity<Tab>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqlServerToTable("Tabs");
        }
      );

      modelBuilder.Entity<Member>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqlServerToTable("Members");
        }
      );

      modelBuilder.Entity<DataType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.JavaScriptEditorClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqlServerToTable("DataTypes");
        }
      );

      modelBuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ForSqlServerToTable("DataSources");
        }
      );

      modelBuilder.Entity<Object>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.ViewName).HasMaxLength(32);
          etb.Property(e => e.Url).HasMaxLength(128);
          etb.ForSqlServerToTable("Objects");
        }
      );

       modelBuilder.Entity<Property>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.ForSqlServerToTable("Properties");
        }
      );

      modelBuilder.Entity<Relation>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.ForSqlServerToTable("Relations");
        }
      );
    }
  }
}