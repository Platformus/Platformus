// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Platformus.Barebone;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.Services.Abstractions;

namespace Platformus.Routing.Services.Defaults
{
  public class DefaultEndpointResolver : IEndpointResolver
  {
    public Endpoint GetEndpoint(IRequestHandler requestHandler, string url)
    {
      IEnumerable<Endpoint> endpoints = this.GetEndpoints(requestHandler);

      foreach (Endpoint endpoint in endpoints)
        if (this.IsMatch(endpoint.UrlTemplate, url))
          return endpoint;

      return null;
    }

    public IEnumerable<KeyValuePair<string, string>> GetArguments(string urlTemplate, string url)
    {
      if (string.IsNullOrEmpty(urlTemplate) || string.IsNullOrEmpty(url))
        return new KeyValuePair<string, string>[] { };

      return this.GetNames(urlTemplate).Zip(this.GetValues(url, urlTemplate), (n, v) => new KeyValuePair<string, string>(n, v));
    }

    private IEnumerable<Endpoint> GetEndpoints(IRequestHandler requestHandler)
    {
      return requestHandler.GetService<ICache>().GetWithDefaultValue(
        "endpoints", () => requestHandler.Storage.GetRepository<IEndpointRepository>().All().ToList()
      );
    }

    private bool IsMatch(string urlTemplate, string url)
    {
      if (urlTemplate == "{*url}" || urlTemplate == url)
        return true;

      if (string.IsNullOrEmpty(urlTemplate) || string.IsNullOrEmpty(url))
        return false;

      return urlTemplate.Count(ch => ch == '/') == url.Count(ch => ch == '/') && Regex.IsMatch(url, this.GetRegexFromUrlTemplate(urlTemplate));
    }

    private IEnumerable<string> GetNames(string urlTemplate)
    {
      return Regex.Matches(urlTemplate, "{.+?}").Cast<Match>().Select(m => m.Value.Replace("{", string.Empty).Replace("}", string.Empty));
    }

    private IEnumerable<string> GetValues(string url, string urlTemplate)
    {
      return Regex.Match(url, this.GetRegexFromUrlTemplate(urlTemplate)).Groups.Cast<Group>().Skip(1).Select(g => g.Value);
    }

    private string GetRegexFromUrlTemplate(string urlTemplate)
    {
      return Regex.Replace(urlTemplate, "{.+?}", "(.+)");
    }
  }
}