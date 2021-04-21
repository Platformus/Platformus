// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Frontend.DataProviders
{
  public class ObjectsDataProvider : DataProviderBase
  {
    public override IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("ClassId", "Class of the objects to load", JavaScriptEditorClassNames.ClassSelector, null, true)
        ),
      };

    public override string Description => "Loads objects of the given class. Supports filtering, sorting, and paging.";

    public override async Task<dynamic> GetDataAsync(HttpContext httpContext, DataSource dataSource)
    {
      IEnumerable<Object> objects = await httpContext.GetStorage().GetRepository<int, Object, ObjectFilter>().GetAllAsync(
        new ObjectFilter(@class: new ClassFilter(id: new ParametersParser(dataSource.DataProviderParameters).GetIntParameterValue("ClassId"))),
        inclusions: new Inclusion<Object>[]
        {
          new Inclusion<Object>("Properties.Member"),
          new Inclusion<Object>("Properties.StringValue.Localizations")
        }
      );

      return objects.Select(o => this.CreateViewModel(o));
    }
  }
}