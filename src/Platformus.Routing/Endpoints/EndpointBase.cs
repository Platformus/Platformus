// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Barebone.Parameters;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Endpoints
{
  public abstract class EndpointBase : IEndpoint
  {
    protected IRequestHandler requestHandler;
    protected Endpoint endpoint;
    protected IEnumerable<KeyValuePair<string, string>> arguments;
    private Dictionary<string, string> parameterValuesByCodes;

    public virtual IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };
    public virtual string Description => null;

    public IActionResult Invoke(IRequestHandler requestHandler, Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> arguments)
    {
      this.requestHandler = requestHandler;
      this.endpoint = endpoint;
      this.arguments = arguments;
      return this.Invoke();
    }

    protected abstract IActionResult Invoke();

    protected bool HasParameter(string key)
    {
      this.CacheParameterValuesByCodes();
      return this.parameterValuesByCodes.ContainsKey(key);
    }

    protected int GetIntParameterValue(string key)
    {
      this.CacheParameterValuesByCodes();

      if (int.TryParse(this.parameterValuesByCodes[key], out int result))
        return result;

      return 0;
    }

    protected bool GetBoolParameterValue(string key)
    {
      this.CacheParameterValuesByCodes();

      if (bool.TryParse(this.parameterValuesByCodes[key], out bool result))
        return result;

      return false;
    }

    protected string GetStringParameterValue(string key)
    {
      this.CacheParameterValuesByCodes();
      return this.parameterValuesByCodes[key];
    }

    private void CacheParameterValuesByCodes()
    {
      if (this.parameterValuesByCodes == null)
        this.parameterValuesByCodes = ParametersParser.Parse(this.endpoint.Parameters).ToDictionary(a => a.Key, a => a.Value);
    }
  }
}