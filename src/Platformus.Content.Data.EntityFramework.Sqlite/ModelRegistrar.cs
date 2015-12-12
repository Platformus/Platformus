
using Microsoft.Data.Entity;
using Platformus.Content.Data.Models;
using ExtCore.Data.EntityFramework.Sqlite;

namespace Platformus.Content.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<CachedObject>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.ObjectId });
          etb.ForSqliteToTable("CachedObjects");
        }
      );

      modelbuilder.Entity<Class>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Classes");
        }
      );

      modelbuilder.Entity<Tab>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Tabs");
        }
      );

      modelbuilder.Entity<Member>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Members");
        }
      );

      modelbuilder.Entity<DataType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("DataTypes");
        }
      );

      modelbuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("DataSources");
        }
      );

      modelbuilder.Entity<Object>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Objects");
        }
      );

       modelbuilder.Entity<Property>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Properties");
        }
      );

      modelbuilder.Entity<Relation>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Relations");
        }
      );
    }
  }
}