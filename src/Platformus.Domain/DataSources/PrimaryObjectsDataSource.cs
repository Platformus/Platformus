// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class PrimaryObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<SerializedObject> GetSerializedObjects(IRequestHandler requestHandler, SerializedObject serializedPage, params KeyValuePair<string, string>[] args)
    {
      if (this.HasArgument(args, "MemberId"))
        return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(CultureManager.GetCurrentCulture(requestHandler.Storage).Id, this.GetIntArgument(args, "MemberId"), serializedPage.ObjectId);

      return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(CultureManager.GetCurrentCulture(requestHandler.Storage).Id, serializedPage.ObjectId);
    }

    public override IEnumerable<Object> GetObjects(IRequestHandler requestHandler, Object page, params KeyValuePair<string, string>[] args)
    {
      if (this.HasArgument(args, "MemberId"))
        return requestHandler.Storage.GetRepository<IObjectRepository>().Primary(this.GetIntArgument(args, "MemberId"), page.Id);

      return requestHandler.Storage.GetRepository<IObjectRepository>().Primary(page.Id);
    }
  }
}