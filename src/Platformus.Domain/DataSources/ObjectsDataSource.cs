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
  public class ObjectsDataSource : DataSourceBase, IMultipleObjectsDataSource
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

    public IEnumerable<dynamic> GetSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new SerializedObject[] { };

      if (!this.HasArgument(args, "SortingMemberId") || !this.HasArgument(args, "SortingDirection"))
        return this.GetUnsortedSerializedObjects(requestHandler, args);

      return this.GetSortedSerializedObjects(requestHandler, args);
    }

    private IEnumerable<dynamic> GetUnsortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      IEnumerable<SerializedObject> serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        this.GetIntArgument(args, "ClassId")
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      int sortingMemberId = this.GetIntArgument(args, "SortingMemberId");
      string direction = this.GetStringArgument(args, "SortingDirection");
      Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(sortingMemberId);
      DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);
      IEnumerable<SerializedObject> serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByClassId(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        this.GetIntArgument(args, "ClassId"),
        dataType.StorageDataType,
        sortingMemberId,
        direction
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }
  }
}