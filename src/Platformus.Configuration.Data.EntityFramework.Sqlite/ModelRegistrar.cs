
using Microsoft.Data.Entity;
using Platformus.Configuration.Data.Models;
using ExtCore.Data.EntityFramework.Sqlite;

namespace Platformus.Configuration.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<Section>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Sections");
        }
      );

      modelbuilder.Entity<Variable>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Variables");
        }
      );
    }
  }
}