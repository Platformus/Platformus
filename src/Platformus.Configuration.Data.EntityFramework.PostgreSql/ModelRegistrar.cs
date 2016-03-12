
using Microsoft.Data.Entity;
using Platformus.Configuration.Data.Models;
using ExtCore.Data.EntityFramework.PostgreSql;

namespace Platformus.Configuration.Data.EntityFramework.PostgreSql
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<Section>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("Sections");
        }
      );

      modelbuilder.Entity<Variable>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForNpgsqlToTable("Variables");
        }
      );
    }
  }
}