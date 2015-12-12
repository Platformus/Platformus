
using Microsoft.Data.Entity;
using ExtCore.Data.EntityFramework.SqlServer;
using Platformus.Static.Data.Models;

namespace Platformus.Static.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<File>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Files");
        }
      );
    }
  }
}