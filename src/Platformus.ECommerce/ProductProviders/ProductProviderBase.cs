// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.ProductProviders
{
  public abstract class ProductProviderBase : IProductProvider
  {
    protected IRequestHandler requestHandler;
    protected Catalog catalog;
    private Dictionary<string, string> parameterValuesByCodes;

    public virtual IEnumerable<ProductProviderParameterGroup> ParameterGroups => new ProductProviderParameterGroup[] { };
    public virtual string Description => null;

    public IEnumerable<Product> GetProducts(IRequestHandler requestHandler, Catalog catalog)
    {
      this.requestHandler = requestHandler;
      this.catalog = catalog;
      return this.GetProducts();
    }

    protected abstract IEnumerable<Product> GetProducts();

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
        this.parameterValuesByCodes = ParametersParser.Parse(this.catalog.Parameters).ToDictionary(a => a.Key, a => a.Value);
    }
  }
}