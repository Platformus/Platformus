// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;
using Platformus.Website.DataProviders;

namespace Platformus.Website.Frontend.DataProviders;

public abstract class DataProviderBase : IDataProvider
{
  public abstract string Description { get; }
  public abstract IEnumerable<ParameterGroup> ParameterGroups { get; }

  public abstract Task<dynamic> GetDataAsync(HttpContext httpContext, DataSource dataSource);

  protected dynamic CreateViewModel(Object @object)
  {
    ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

    expandoObjectBuilder.AddProperty("Id", @object.Id);
    expandoObjectBuilder.AddProperty("ClassId", @object.ClassId);

    foreach (Property property in @object.Properties)
      expandoObjectBuilder.AddProperty(property.Member.Code, property.GetValue());

    return expandoObjectBuilder.Build();
  }
}