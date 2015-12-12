
using Microsoft.Data.Entity;
using Platformus.Configuration.Data.Models;
using ExtCore.Data.EntityFramework.SqlServer;

namespace Platformus.Configuration.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<Section>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Sections");
        }
      );

      modelbuilder.Entity<Variable>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Variables");
        }
      );
    }
  }
}