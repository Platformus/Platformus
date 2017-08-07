// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Domain.DataSources;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.DataSources;
using Platformus.Routing.Endpoints;

namespace Platformus.Domain.Frontend
{
  public class DefaultEndpoint : EndpointBase
  {
    public override IEnumerable<EndpointParameterGroup> EndpointParameterGroups =>
      new EndpointParameterGroup[]
      {
        new EndpointParameterGroup(
          "General",
          new EndpointParameter("ViewName", "View name", "text"),
          new EndpointParameter("UseCaching", "Use caching", "checkbox")
        )
      };

    public override string Description => "Returns specified view with the view model populated using the data sources.";

    public override IActionResult Invoke(IRequestHandler requestHandler, Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> parameters)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));

      if (this.GetBoolArgument(this.GetParameters(endpoint.Parameters), "UseCaching"))
        return requestHandler.HttpContext.RequestServices.GetService<ICache>().GetPageActionResultWithDefaultValue(
          url + requestHandler.HttpContext.Request.QueryString, () => this.GetActionResult(requestHandler, endpoint, parameters, url)
        );

      return this.GetActionResult(requestHandler, endpoint, parameters, url);
    }

    private IActionResult GetActionResult(IRequestHandler requestHandler, Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> parameters, string url)
    {
      dynamic viewModel = this.CreateViewModel(requestHandler, endpoint);

      return (requestHandler as Platformus.Barebone.Frontend.Controllers.ControllerBase).View(this.GetStringArgument(this.GetParameters(endpoint.Parameters), "ViewName"), viewModel);
    }

    private dynamic CreateViewModel(IRequestHandler requestHandler, Endpoint endpoint)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

      foreach (DataSource dataSource in requestHandler.Storage.GetRepository<IDataSourceRepository>().FilteredByEndpointId(endpoint.Id))
      viewModelBuilder.BuildProperty(dataSource.Code, this.CreateDataSourceViewModel(requestHandler, dataSource));

      return viewModelBuilder.Build();
    }

    private dynamic CreateDataSourceViewModel(IRequestHandler requestHandler, DataSource dataSource)
    {
      IDataSource dataSourceInstance = StringActivator.CreateInstance<IDataSource>(dataSource.CSharpClassName);

      if (dataSourceInstance is ISingleObjectDataSource)
        return (dataSourceInstance as ISingleObjectDataSource).GetSerializedObject(
          requestHandler, this.GetParameters(dataSource.Parameters)
        );

      if (dataSourceInstance is IMultipleObjectsDataSource)
        return (dataSourceInstance as IMultipleObjectsDataSource).GetSerializedObjects(
          requestHandler, this.GetParameters(dataSource.Parameters)
        );

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