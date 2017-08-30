// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.DataSources;
using Platformus.Routing.Endpoints;

namespace Platformus.Domain.Frontend
{
  public class DefaultEndpoint : EndpointBase
  {
    public override IEnumerable<EndpointParameterGroup> ParameterGroups =>
      new EndpointParameterGroup[]
      {
        new EndpointParameterGroup(
          "General",
          new EndpointParameter("ViewName", "View name", "textBox", null, true),
          new EndpointParameter("UseCaching", "Use caching", "checkbox")
        )
      };

    public override string Description => "Returns specified view with the view model populated using the data sources.";

    protected override IActionResult GetActionResult(IRequestHandler requestHandler, Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> arguments)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));

      if (this.GetBoolParameterValue("UseCaching"))
        return requestHandler.GetService<ICache>().GetPageActionResultWithDefaultValue(
          url + requestHandler.HttpContext.Request.QueryString, () => this.GetActionResult(requestHandler, endpoint, arguments, url)
        );

      return this.GetActionResult(requestHandler, endpoint, arguments, url);
    }

    private IActionResult GetActionResult(IRequestHandler requestHandler, Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> arguments, string url)
    {
      dynamic viewModel = this.CreateViewModel(requestHandler, endpoint);

      return (requestHandler as Platformus.Barebone.Frontend.Controllers.ControllerBase).View(this.GetStringParameterValue("ViewName"), viewModel);
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

      return dataSourceInstance.GetData(requestHandler, dataSource);
    }
  }
}