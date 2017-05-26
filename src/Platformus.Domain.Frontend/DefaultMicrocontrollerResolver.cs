// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Frontend
{
  public class DefaultMicrocontrollerResolver : IMicrocontrollerResolver
  {
    public Microcontroller GetMicrocontroller(IRequestHandler requestHandler, string url)
    {
      IEnumerable<Microcontroller> microcontrollers = this.GetMicrocontrollers(requestHandler);

      foreach (Microcontroller microcontroller in microcontrollers)
        if (this.IsMatch(microcontroller.UrlTemplate, url))
          return microcontroller;

      return null;
    }

    public IEnumerable<KeyValuePair<string, string>> GetParameters(string urlTemplate, string url)
    {
      if (string.IsNullOrEmpty(urlTemplate) || string.IsNullOrEmpty(url))
        return new KeyValuePair<string, string>[] { };

      return this.GetNames(urlTemplate).Zip(this.GetValues(url, urlTemplate), (n, v) => new KeyValuePair<string, string>(n, v));
    }

    private IEnumerable<Microcontroller> GetMicrocontrollers(IRequestHandler requestHandler)
    {
      return requestHandler.HttpContext.RequestServices.GetService<ICache>().GetWithDefaultValue(
        "microcontrollers", requestHandler.Storage.GetRepository<IMicrocontrollerRepository>().All()
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