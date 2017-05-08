// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.DataSources
{
  public class ObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<SerializedObject> GetSerializedObjects(IRequestHandler requestHandler, SerializedObject serializedPage, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new SerializedObject[] { };

      return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(this.GetIntArgument(args, "ClassId"));
    }

    public override IEnumerable<Object> GetObjects(IRequestHandler requestHandler, Object page, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new Object[] { };

      return requestHandler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(this.GetIntArgument(args, "ClassId"));
    }
  }
}