// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Content.DataSources;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Content.Frontend.ViewModels.Shared
{
  public class ObjectViewModelBuilder : ViewModelBuilderBase
  {
    public ObjectViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public ObjectViewModel Build(Object @object)
    {
      Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId);
      IDictionary<string, DataSourceViewModel> dataSourceInstances = new Dictionary<string, DataSourceViewModel>();

      foreach (DataSource dataSource in this.handler.Storage.GetRepository<IDataSourceRepository>().FilteredByClassId(@class.Id))
      {
        System.Type type = this.GetType(dataSource.CSharpClassName);

        if (type == null)
          throw new System.ArgumentException("Type " + dataSource.CSharpClassName + " not found");

        IDataSource dataSourceInstance = System.Activator.CreateInstance(type) as IDataSource;

        dataSourceInstance.Initialize(this.handler, @object, this.GetParameters(dataSource.Parameters));
        dataSourceInstances.Add(dataSource.Code, new DataSourceViewModelBuilder(this.handler).Build(dataSourceInstance.GetObjects()));
      }

      return new ObjectViewModel()
      {
        Id = @object.Id,
        Class = new ClassViewModelBuilder(this.handler).Build(@class.Id, @class.ViewName),
        Url = @object.Url,
        Properties = this.handler.Storage.GetRepository<IPropertyRepository>().FilteredByObjectId(@object.Id).ToDictionary(
          p => this.handler.Storage.GetRepository<IMemberRepository>().WithKey(p.MemberId).Code,
          p => new PropertyViewModelBuilder(this.handler).Build(p)
        ),
        DataSources = dataSourceInstances
      };
    }

    public ObjectViewModel Build(CachedObject cachedObject)
    {
      IDictionary<string, DataSourceViewModel> dataSourceInstances = new Dictionary<string, DataSourceViewModel>();

      if (!string.IsNullOrEmpty(cachedObject.CachedDataSources))
      {
        foreach (CachedDataSource cachedDataSource in JsonConvert.DeserializeObject<IEnumerable<CachedDataSource>>(cachedObject.CachedDataSources))
        {
          System.Type type = this.GetType(cachedDataSource.CSharpClassName);

          if (type == null)
            throw new System.ArgumentException("Type " + cachedDataSource.CSharpClassName + " not found");

          IDataSource dataSourceInstance = System.Activator.CreateInstance(type) as IDataSource;

          dataSourceInstance.Initialize(this.handler, cachedObject, this.GetParameters(cachedDataSource.Parameters));
          dataSourceInstances.Add(cachedDataSource.Code, new DataSourceViewModelBuilder(this.handler).Build(dataSourceInstance.GetCachedObjects()));
        }
      }

      IEnumerable<CachedProperty> cachedProperties = JsonConvert.DeserializeObject<IEnumerable<CachedProperty>>(cachedObject.CachedProperties);

      return new ObjectViewModel()
      {
        Id = cachedObject.ObjectId,
        Class = new ClassViewModelBuilder(this.handler).Build(cachedObject.ClassId, cachedObject.ClassViewName),
        Url = cachedObject.Url,
        Properties = cachedProperties.ToDictionary(
          cp => cp.MemberCode,
          cp => new PropertyViewModelBuilder(this.handler).Build(cp)
        ),
        DataSources = dataSourceInstances
      };
    }

    private System.Type GetType(string name)
    {
      foreach (Assembly assembly in ExtensionManager.Assemblies)
        foreach (System.Type type in assembly.GetTypes())
          if (type.FullName == name)
            return type;

      return null;
    }

    private KeyValuePair<string, string>[] GetParameters(string parameters)
    {
      List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

      if (!string.IsNullOrEmpty(parameters))
      {
        foreach (string parameter in parameters.Split(';').Select(p => p.Trim()))
        {
          if (!string.IsNullOrEmpty(parameter))
          {
            string[] operands = parameter.Trim().Split('=');

            result.Add(new KeyValuePair<string, string>(operands[0], operands[1]));
          }
        }
      }

      return result.ToArray();
    }
  }
}