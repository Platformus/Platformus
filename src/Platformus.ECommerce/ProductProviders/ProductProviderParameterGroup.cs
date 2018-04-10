// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.ECommerce.ProductProviders
{
  public class ProductProviderParameterGroup
  {
    public string Name { get; set; }
    public IEnumerable<ProductProviderParameter> Parameters { get; set; }

    public ProductProviderParameterGroup(string name, params ProductProviderParameter[] parameters)
    {
      this.Name = name;
      this.Parameters = parameters;
    }
  }
}