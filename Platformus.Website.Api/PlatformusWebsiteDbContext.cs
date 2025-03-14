// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Platformus.Website.Data.Entities;

namespace Platformus.Website;

public static class PlatformusWebsiteDbContext
{
  public static void OnModelCreating(DbContext dbContext, ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Menu>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
      etb.ToTable("Menus");
    });

    modelBuilder.Entity<MenuItem>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.MenuId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      etb.ToTable("MenuItems");
    });

    modelBuilder.Entity<LocalizedMenuItem>(etb => {
      etb.HasKey(e => new { e.MenuItemId, e.CultureId });
      etb.Property(e => e.CultureId).IsRequired().HasMaxLength(2);
      etb.ToTable("LocalizedMenuItems");
    });

    modelBuilder.Entity<Form>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
      etb.ToTable("Forms");
    });

    modelBuilder.Entity<FieldType>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).IsRequired().HasMaxLength(16);
      etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
      etb.ToTable("FieldTypes");
    });

    modelBuilder.Entity<Field>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.FormId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      etb.ToTable("Fields");
    });

    modelBuilder.Entity<LocalizedField>(etb => {
      etb.HasKey(e => new { e.FieldId, e.CultureId });
      etb.Property(e => e.CultureId).IsRequired().HasMaxLength(2);
      etb.ToTable("LocalizedFields");
    });

    modelBuilder.Entity<Class>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.SingularName).IsRequired().HasMaxLength(64);
      etb.Property(e => e.PluralName).IsRequired().HasMaxLength(64);
      etb.Property(e => e.UrlSegment).IsRequired().HasMaxLength(64);
      etb.Property(e => e.CSharpName).HasMaxLength(32);
      etb.ToTable("Classes");
    });

    modelBuilder.Entity<MemberType>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).IsRequired().HasMaxLength(16);
      etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
      etb.ToTable("MemberTypes");
    });

    modelBuilder.Entity<Member>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.ClassId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
      etb.Property(e => e.CSharpName).HasMaxLength(32);
      etb.ToTable("Members");
    });

    modelBuilder.Entity<Platformus.Website.Data.Entities.Object>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.ClassId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      etb.ToTable("Objects");
    });

    modelBuilder.Entity<Property>(etb => {
      etb.HasKey(e => e.Id);
      etb.Property(e => e.Id).UseIdentityColumn();
      etb.Property(e => e.ObjectId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      etb.Property(e => e.MemberId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      etb.ToTable("Properties");
    });

    modelBuilder.Entity<LocalizedProperty>(etb => {
      etb.HasKey(e => new { e.PropertyId, e.CultureId });
      etb.Property(e => e.CultureId).IsRequired().HasMaxLength(2);
      etb.ToTable("LocalizedProperties");
    });
  }
}
