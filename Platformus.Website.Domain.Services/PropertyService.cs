// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Platformus.Website.Domain.Models;
using Platformus.Website.Domain.Services.Extensions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Services;

public class PropertyService : Service<int, Data.Entities.Property, Property, PropertyFilter>
{
  public PropertyService(DbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
  {
  }

  public override async Task EditAsync(Property property)
  {
    IEnumerable<LocalizedProperty>? localizedProperties = property.LocalizedProperties?.ToList();

    property.LocalizedProperties = null;

    await base.EditAsync(property);

    if (localizedProperties != null)
    {
      foreach (LocalizedProperty localizedProperty in localizedProperties)
        localizedProperty.Property = property;

      await this.MergeLocalizedPropertiesAsync(localizedProperties!);
    } 
  }

  public async Task MergeLocalizedPropertiesAsync(IEnumerable<LocalizedProperty> localizedProperties)
  {
    IEnumerable<SqlParameter> parameters =
    [
      new SqlParameter("@LocalizedProperties", localizedProperties.Select(lp => lp.ToEntity()).ToDataTable()) { TypeName = "LocalizedPropertiesType" }
    ];

    await this.dbContext.Database.ExecuteSqlRawAsync("MergeLocalizedProperties @LocalizedProperties", parameters);
  }
}