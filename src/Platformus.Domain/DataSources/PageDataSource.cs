// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Routing.Data.Entities;

namespace Platformus.Domain.DataSources
{
  public class PageDataSource : DataSourceBase
  {
    public override string Description => "Loads current page’s object by URL.";

    protected override dynamic GetRawData(IRequestHandler requestHandler, DataSource dataSource)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));
      SerializedObject serializedObject = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id, url
      );

      if (serializedObject == null)
        throw new HttpException(404, "Not found.");

      return this.CreateSerializedObjectViewModel(serializedObject);
    }
  }
}