// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Platformus.Core;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;
using Platformus.Website.DataProviders;
using Platformus.Website.RequestProcessors;

namespace Platformus.Website.Frontend.RequestProcessors
{
  public class DefaultRequestProcessor : IRequestProcessor
  {
    public IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("ViewName", "View name", Core.JavaScriptEditorClassNames.TextBox, null, true)
        )
      };

    public string Description => "Returns specified view with the view model populated using the data sources.";

    public async Task<IActionResult> ProcessAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint)
    {
      dynamic viewModel = await this.CreateViewModelAsync(httpContext, endpoint);

      if (viewModel == null)
        return null;

      return new ViewResult()
      {
        ViewName = new ParametersParser(endpoint.RequestProcessorParameters).GetStringParameterValue("ViewName"),
        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = viewModel }
      };
    }

    private async Task<dynamic> CreateViewModelAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint)
    {
      ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

      foreach (DataSource dataSource in endpoint.DataSources)
      {
        dynamic viewModel = await this.GetDataProvider(dataSource).GetDataAsync(httpContext, dataSource);

        if (viewModel == null)
          return null;

        expandoObjectBuilder.AddProperty(dataSource.Code, viewModel);
      }

      return expandoObjectBuilder.Build();
    }

    private IDataProvider GetDataProvider(DataSource dataSource)
    {
      return StringActivator.CreateInstance<IDataProvider>(dataSource.DataProviderCSharpClassName);
    }
  }
}