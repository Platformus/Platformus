
using Microsoft.Data.Entity;
using ExtCore.Data.EntityFramework.Sqlite;
using Platformus.Static.Data.Models;

namespace Platformus.Static.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<File>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Files");
        }
      );
    }
  }
}