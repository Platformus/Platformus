// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class ObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<DataSourceParameter> DataSourceParameters
    {
      get
      {
        return new DataSourceParameter[]
        {
          new DataSourceParameter("ClassId", "Class ID", "temp"),
          new DataSourceParameter("SortingMemberId", "Sorting member ID", "temp"),
          new DataSourceParameter("SortingDirection", "Sorting direction", "temp")
        };
      }
    }

    public override IEnumerable<SerializedObject> GetSerializedObjects(IRequestHandler requestHandler, SerializedObject serializedPage, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new SerializedObject[] { };

      if (!this.HasArgument(args, "SortingMemberId") || !this.HasArgument(args, "SortingDirection"))
        return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(CultureManager.GetCurrentCulture(requestHandler.Storage).Id, this.GetIntArgument(args, "ClassId")).ToList();

      int orderBy = this.GetIntArgument(args, "SortingMemberId");
      string direction = this.GetStringArgument(args, "SortingDirection");
      Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(orderBy);
      DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

      return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(CultureManager.GetCurrentCulture(requestHandler.Storage).Id, this.GetIntArgument(args, "ClassId"), dataType.StorageDataType, orderBy, direction).ToList();
    }

    public override IEnumerable<Object> GetObjects(IRequestHandler requestHandler, Object page, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new Object[] { };

      if (!this.HasArgument(args, "SortingMemberId") || !this.HasArgument(args, "SortingDirection"))
        return requestHandler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(this.GetIntArgument(args, "ClassId")).ToList();

      int orderBy = this.GetIntArgument(args, "SortingMemberId");
      string direction = this.GetStringArgument(args, "SortingDirection");
      Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(orderBy);
      DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

      return requestHandler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(this.GetIntArgument(args, "ClassId"), dataType.StorageDataType, orderBy, direction, CultureManager.GetCurrentCulture(requestHandler.Storage).Id).ToList();
    }
  }
}