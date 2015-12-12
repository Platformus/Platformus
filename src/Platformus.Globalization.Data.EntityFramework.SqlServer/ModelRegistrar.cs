
using Microsoft.Data.Entity;
using ExtCore.Data.EntityFramework.SqlServer;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<Dictionary>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Dictionaries");
        }
      );

      modelbuilder.Entity<Culture>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Cultures");
        }
      );

      modelbuilder.Entity<Localization>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Localizations");
        }
      );
    }
  }
}