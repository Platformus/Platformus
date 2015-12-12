
using Microsoft.Data.Entity;
using ExtCore.Data.EntityFramework.Sqlite;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<Dictionary>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Dictionaries");
        }
      );

      modelbuilder.Entity<Culture>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Cultures");
        }
      );

      modelbuilder.Entity<Localization>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Localizations");
        }
      );
    }
  }
}