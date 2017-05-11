// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Domain.DataSources;
using Platformus.Globalization;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain.Frontend
{
  public class DefaultMicrocontroller : IMicrocontroller
  {
    public IActionResult Invoke(IRequestHandler requestHandler, Microcontroller microcontroller, IEnumerable<KeyValuePair<string, string>> parameters)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));
      dynamic viewModel = null;

      SerializedObject serializedPage = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id, url
      );

      if (serializedPage != null)
        viewModel = this.CreateViewModel(requestHandler, microcontroller, serializedPage);

      else
      {
        Object page = requestHandler.Storage.GetRepository<IObjectRepository>().WithUrl(url);

        if (page != null)
          viewModel = this.CreateViewModel(requestHandler, microcontroller, page);
      }

      if (viewModel != null)
        return (requestHandler as Platformus.Barebone.Frontend.Controllers.ControllerBase).View(microcontroller.ViewName, viewModel);

      return null;
    }

    private dynamic CreateViewModel(IRequestHandler requestHandler, Microcontroller microcontroller, SerializedObject page)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

      viewModelBuilder.BuildProperty("Page", this.CreateObjectViewModel(requestHandler, page));

      foreach (DataSource dataSource in requestHandler.Storage.GetRepository<IDataSourceRepository>().FilteredByMicrocontrollerId(microcontroller.Id))
        viewModelBuilder.BuildProperty(dataSource.Code, this.CreateDataSourceViewModel(requestHandler, page, dataSource));

      return viewModelBuilder.Build();
    }

    private dynamic CreateObjectViewModel(IRequestHandler requestHandler, SerializedObject serializedObject)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

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

    private IEnumerable<dynamic> CreateDataSourceViewModel(IRequestHandler requestHandler, SerializedObject serializedPage, DataSource dataSource)
    {
      IDataSource dataSourceInstance = StringActivator.CreateInstance<IDataSource>(dataSource.CSharpClassName);

      return dataSourceInstance.GetSerializedObjects(requestHandler, serializedPage, this.GetParameters(dataSource.Parameters)).Select(
        so => this.CreateObjectViewModel(requestHandler, so)
      );
    }

    private dynamic CreateViewModel(IRequestHandler requestHandler, Microcontroller microcontroller, Object page)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

      viewModelBuilder.BuildProperty("Page", this.CreateObjectViewModel(requestHandler, page));

      foreach (DataSource dataSource in requestHandler.Storage.GetRepository<IDataSourceRepository>().FilteredByMicrocontrollerId(microcontroller.Id))
        viewModelBuilder.BuildProperty(dataSource.Code, this.CreateDataSourceViewModel(requestHandler, page, dataSource));

      return viewModelBuilder.Build();
    }

    private dynamic CreateObjectViewModel(IRequestHandler requestHandler, Object @object)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

      foreach (var property in requestHandler.Storage.GetRepository<IPropertyRepository>().FilteredByObjectId(@object.Id))
      {
        Member member = requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(property.MemberId);
        DataType dataType = requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

        if (dataType.StorageDataType == StorageDataType.Integer)
          viewModelBuilder.BuildProperty(member.Code, property.IntegerValue);

        else if (dataType.StorageDataType == StorageDataType.Decimal)
          viewModelBuilder.BuildProperty(member.Code, property.DecimalValue);

        else if (dataType.StorageDataType == StorageDataType.String)
        {
          Culture neutralCulture = CultureManager.GetNeutralCulture(requestHandler.Storage);
          string stringValue = member.IsPropertyLocalizable == true ?
            requestHandler.GetLocalizationValue((int)property.StringValueId) : requestHandler.GetLocalizationValue((int)property.StringValueId, neutralCulture.Id);

          viewModelBuilder.BuildProperty(member.Code, stringValue);
        }

        else if (dataType.StorageDataType == StorageDataType.DateTime)
          viewModelBuilder.BuildProperty(member.Code, property.DateTimeValue);
      }

      return viewModelBuilder.Build();
    }

    private IEnumerable<dynamic> CreateDataSourceViewModel(IRequestHandler requestHandler, Object page, DataSource dataSource)
    {
      IDataSource dataSourceInstance = StringActivator.CreateInstance<IDataSource>(dataSource.CSharpClassName);

      return dataSourceInstance.GetObjects(requestHandler, page, this.GetParameters(dataSource.Parameters)).Select(
        o => this.CreateObjectViewModel(requestHandler, o)
      );
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