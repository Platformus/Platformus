// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Barebone.Parameters;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.DataSources;
using Platformus.Routing.Endpoints;

namespace Platformus.Domain.Frontend
{
  public class DefaultEndpoint : EndpointBase
  {
    public override IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("ViewName", "View name", "textBox", null, true),
          new Parameter("UseCaching", "Use caching", "checkbox")
        )
      };

    public override string Description => "Returns specified view with the view model populated using the data sources.";

    protected override IActionResult Invoke()
    {
      string url = string.Format("/{0}", this.requestHandler.HttpContext.GetRouteValue("url"));

      if (this.GetBoolParameterValue("UseCaching"))
        return this.requestHandler.GetService<ICache>().GetPageActionResultWithDefaultValue(
          url + this.requestHandler.HttpContext.Request.QueryString, () => this.GetActionResult(endpoint, arguments, url)
        );

      return this.GetActionResult(endpoint, arguments, url);
    }

    private IActionResult GetActionResult(Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> arguments, string url)
    {
      dynamic viewModel = this.CreateViewModel(endpoint);

      return (this.requestHandler as Platformus.Barebone.Frontend.Controllers.ControllerBase).View(this.GetStringParameterValue("ViewName"), viewModel);
    }

    private dynamic CreateViewModel(Endpoint endpoint)
    {
      ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

      foreach (DataSource dataSource in this.requestHandler.Storage.GetRepository<IDataSourceRepository>().FilteredByEndpointId(endpoint.Id).ToList())
        expandoObjectBuilder.AddProperty(dataSource.Code, this.CreateDataSourceViewModel(dataSource));

      return expandoObjectBuilder.Build();
    }

    private dynamic CreateDataSourceViewModel(DataSource dataSource)
    {
      IDataSource dataSourceInstance = StringActivator.CreateInstance<IDataSource>(dataSource.CSharpClassName);

      return dataSourceInstance.GetData(this.requestHandler, dataSource);
    }
  }
}