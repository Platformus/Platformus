// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.FileManager.Data.Models;

namespace Platformus.FileManager.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<File>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ForSqlServerUseSequenceHiLo();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ForSqlServerToTable("Files");
        }
      );
    }
  }
}