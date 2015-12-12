
using Microsoft.Data.Entity;
using ExtCore.Data.EntityFramework.Sqlite;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<CachedForm>(etb =>
        {
          etb.HasKey(e => e.FormId);
          etb.Property(e => e.FormId);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("CachedForms");
        }
      );

      modelbuilder.Entity<Form>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Forms");
        }
      );

      modelbuilder.Entity<FieldType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("FieldTypes");
        }
      );

      modelbuilder.Entity<Field>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Fields");
        }
      );

      modelbuilder.Entity<FieldOption>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("FieldOptions");
        }
      );
    }
  }
}