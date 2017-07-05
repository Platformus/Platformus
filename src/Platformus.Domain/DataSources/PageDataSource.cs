// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class PageDataSource : DataSourceBase, ISingleObjectDataSource
  {
    public dynamic GetSerializedObject(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));
      SerializedObject serializedObject = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id, url
      );

      return this.CreateSerializedObjectViewModel(serializedObject);
    }
  }
}