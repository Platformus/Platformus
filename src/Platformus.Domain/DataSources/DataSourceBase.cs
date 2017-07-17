// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.DataSources
{
  public abstract class DataSourceBase : IDataSource
  {
    private Dictionary<string, string> args;

    public virtual IEnumerable<DataSourceParameter> DataSourceParameters
    {
      get
      {
        return new DataSourceParameter[] { };
      }
    }

    protected bool HasArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);
      return this.args.ContainsKey(key);
    }

    protected int GetIntArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);

      if (int.TryParse(this.args[key], out int result))
        return result;

      return 0;
    }

    protected string GetStringArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);
      return this.args[key];
    }

    protected Params GetParams(IRequestHandler requestHandler, KeyValuePair<string, string>[] args)
    {
      int sortingMemberId = this.GetIntArgument(args, "SortingMemberId");
      string sortingDirection = this.GetStringArgument(args, "SortingDirection");
      Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(sortingMemberId);
      DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

      return new Params(sorting: new Sorting(dataType.StorageDataType, sortingMemberId, sortingDirection));
    }

    protected dynamic CreateSerializedObjectViewModel(SerializedObject serializedObject)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

      viewModelBuilder.BuildId(serializedObject.ObjectId);

      foreach (SerializedProperty serializedProperty in JsonConvert.DeserializeObject<IEnumerable<SerializedProperty>>(serializedObject.SerializedProperties))
      {
        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Integer)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.IntegerValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Decimal)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.DecimalValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.String)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.StringValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.DateTime)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.DateTimeValue);
      }

      return viewModelBuilder.Build();
    }

    private void CacheArguments(KeyValuePair<string, string>[] args)
    {
      if (this.args == null)
        this.args = args.ToDictionary(a => a.Key, a => a.Value);
    }
  }
}