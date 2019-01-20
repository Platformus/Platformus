// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Domain.Frontend.DataSources
{
  public class PageObjectDataSource : DataSourceBase
  {
    public override string Description => "Loads current page’s object by URL.";

    protected override dynamic GetData()
    {
      string url = string.Format("/{0}", this.requestHandler.HttpContext.GetRouteValue("url"));
      SerializedObject serializedObject = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id, url
      );

      if (serializedObject == null)
        throw new HttpException(404, "Not found.");

      return this.CreateSerializedObjectViewModel(serializedObject);
    }
  }
}