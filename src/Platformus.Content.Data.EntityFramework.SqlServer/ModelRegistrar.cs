
using Microsoft.Data.Entity;
using Platformus.Content.Data.Models;
using ExtCore.Data.EntityFramework.SqlServer;

namespace Platformus.Content.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<CachedObject>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.ObjectId });
          etb.ForSqlServerToTable("CachedObjects");
        }
      );

      modelbuilder.Entity<Class>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Classes");
        }
      );

      modelbuilder.Entity<Tab>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Tabs");
        }
      );

      modelbuilder.Entity<Member>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Members");
        }
      );

      modelbuilder.Entity<DataType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("DataTypes");
        }
      );

      modelbuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("DataSources");
        }
      );

      modelbuilder.Entity<Object>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Objects");
        }
      );

       modelbuilder.Entity<Property>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Properties");
        }
      );

      modelbuilder.Entity<Relation>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Relations");
        }
      );
    }
  }
}