
using Microsoft.Data.Entity;
using ExtCore.Data.EntityFramework.SqlServer;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<CachedMenu>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.MenuId });
          etb.ForSqlServerToTable("CachedMenus");
        }
      );

      modelbuilder.Entity<Menu>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Menus");
        }
      );

      modelbuilder.Entity<MenuItem>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("MenuItems");
        }
      );
    }
  }
}