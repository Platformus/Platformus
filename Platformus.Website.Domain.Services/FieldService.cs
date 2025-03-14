// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Platformus.Website.Domain.Models;
using Platformus.Website.Domain.Services.Extensions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Services;

public class FieldService : Service<int, Data.Entities.Field, Field, FieldFilter>
{
  public FieldService(DbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
  {
  }

  public override async Task EditAsync(Field field)
  {
    IEnumerable<LocalizedField>? localizedFields = field.LocalizedFields?.ToList();

    field.LocalizedFields = null;

    await base.EditAsync(field);

    if (localizedFields != null)
    {
      foreach (LocalizedField localizedField in localizedFields)
        localizedField.Field = field;

      await this.MergeLocalizedFieldsAsync(localizedFields!);
    } 
  }

  public async Task MergeLocalizedFieldsAsync(IEnumerable<LocalizedField> localizedFields)
  {
    IEnumerable<SqlParameter> parameters =
    [
      new SqlParameter("@LocalizedFields", localizedFields.Select(lf => lf.ToEntity()).ToDataTable()) { TypeName = "LocalizedFieldsType" }
    ];

    await this.dbContext.Database.ExecuteSqlRawAsync("MergeLocalizedFields @LocalizedFields", parameters);
  }
}