// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Core.Filters;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Frontend.DataSources
{
  public class PageObjectDataSource : DataSourceBase
  {
    public override IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };
    public override string Description => "Loads current page’s object by URL.";

    public override async Task<dynamic> GetDataAsync(HttpContext httpContext, DataSource dataSource)
    {
      Object @object = (await httpContext.GetStorage().GetRepository<int, Object, ObjectFilter>().GetAllAsync(
        new ObjectFilter() { StringValue = new LocalizationFilter() { Value = new StringFilter() { Equals = httpContext.Request.GetUrl() } } },
        inclusions: new Inclusion<Object>[]
        {
          new Inclusion<Object>("Properties.Member"),
          new Inclusion<Object>("Properties.StringValue.Localizations")
        }
      )).FirstOrDefault();

      if (@object == null)
        return null;

      return this.CreateViewModel(@object);
    }
  }
}