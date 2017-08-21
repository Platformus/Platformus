// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.FileManager.Data.Entities;

namespace Platformus.FileManager.Data.EntityFramework.Sqlite
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<File>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Files");
        }
      );
    }
  }
}