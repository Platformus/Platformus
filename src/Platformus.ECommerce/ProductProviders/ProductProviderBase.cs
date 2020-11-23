﻿// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core;
using Platformus.Core.Parameters;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.ProductProviders
{
  public abstract class ProductProviderBase : IProductProvider
  {
    protected IRequestHandler requestHandler;
    protected Catalog catalog;
    protected int[] attributeIds;
    private Dictionary<string, string> parameterValuesByCodes;

    public virtual IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };
    public virtual string Description => null;

    public IEnumerable<SerializedProduct> GetProducts(IRequestHandler requestHandler, Catalog catalog)
    {
      this.requestHandler = requestHandler;
      this.catalog = catalog;
      return this.GetProducts();
    }

    public IEnumerable<SerializedProduct> GetProducts(IRequestHandler requestHandler, Catalog catalog, int[] attributeIds)
    {
      this.requestHandler = requestHandler;
      this.catalog = catalog;
      this.attributeIds = attributeIds;
      return this.GetProducts(attributeIds);
    }

    public IEnumerable<SerializedAttribute> GetAttributes(IRequestHandler requestHandler, Catalog catalog)
    {
      this.requestHandler = requestHandler;
      this.catalog = catalog;
      return this.GetAttributes();
    }

    protected abstract IEnumerable<SerializedProduct> GetProducts();
    protected abstract IEnumerable<SerializedProduct> GetProducts(int[] attributeIds);
    protected abstract IEnumerable<SerializedAttribute> GetAttributes();

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