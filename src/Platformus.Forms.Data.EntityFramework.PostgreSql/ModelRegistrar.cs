// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.PostgreSql;
using Microsoft.Data.Entity;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.EntityFramework.PostgreSql
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<CachedForm>(etb =>
        {
          etb.HasKey(e => e.FormId);
          etb.Property(e => e.FormId);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("CachedForms");
        }
      );

      modelbuilder.Entity<Form>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("Forms");
        }
      );

      modelbuilder.Entity<FieldType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("FieldTypes");
        }
      );

      modelbuilder.Entity<Field>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("Fields");
        }
      );

      modelbuilder.Entity<FieldOption>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("FieldOptions");
        }
      );
    }
  }
}