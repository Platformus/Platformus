// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Services;

public class Url : IUrl
{
  private readonly NavigationManager navigationManager;
  private readonly IDictionary<string, string> parameters;
  private readonly IList<string> skippedParameters;

  public Url(NavigationManager navigationManager)
  {
    this.navigationManager = navigationManager;
    this.parameters = new Dictionary<string, string>();
    this.skippedParameters = new List<string>();
  }

  public Url Set(string name, string value)
  {
    this.parameters[name] = value;
    return this;
  }

  public Url Remove(string name)
  {
    this.skippedParameters.Add(name);
    return this;
  }

  public string Build()
  {
    Uri url = new Uri(this.navigationManager.Uri);
    IDictionary<string, string> parameters = QueryHelpers.ParseQuery(url.Query)
      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

    foreach (KeyValuePair<string, string> parameter in this.parameters)
      parameters[parameter.Key] = parameter.Value;

    foreach (string skippedParameter in this.skippedParameters)
      parameters.Remove(skippedParameter);

    string baseUrl = url.GetLeftPart(UriPartial.Path);
    return QueryHelpers.AddQueryString(baseUrl, parameters!);
  }

  public void Go()
  {
    string url = this.Build();

    this.navigationManager.NavigateTo(url);
  }
}
