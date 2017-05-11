// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class ObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<SerializedObject> GetSerializedObjects(IRequestHandler requestHandler, SerializedObject serializedPage, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new SerializedObject[] { };

      if (!this.HasArgument(args, "OrderBy") || !this.HasArgument(args, "Direction"))
        return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(CultureManager.GetCurrentCulture(requestHandler.Storage).Id, this.GetIntArgument(args, "ClassId"));

      int orderBy = this.GetIntArgument(args, "OrderBy");
      string direction = this.GetStringArgument(args, "Direction");
      Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(orderBy);
      DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

      return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(CultureManager.GetCurrentCulture(requestHandler.Storage).Id, this.GetIntArgument(args, "ClassId"), dataType.StorageDataType, orderBy, direction);
    }

    public override IEnumerable<Object> GetObjects(IRequestHandler requestHandler, Object page, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new Object[] { };

      if (!this.HasArgument(args, "OrderBy") || !this.HasArgument(args, "Direction"))
        return requestHandler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(this.GetIntArgument(args, "ClassId"));

      int orderBy = this.GetIntArgument(args, "OrderBy");
      string direction = this.GetStringArgument(args, "Direction");
      Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(orderBy);
      DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

      return requestHandler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(this.GetIntArgument(args, "ClassId"), dataType.StorageDataType, orderBy, direction, CultureManager.GetCurrentCulture(requestHandler.Storage).Id);
    }
  }
}