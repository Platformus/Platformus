// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.ProductProviders
{
  public interface IProductProvider
  {
    IEnumerable<ProductProviderParameterGroup> ParameterGroups { get; }
    string Description { get; }

    IEnumerable<SerializedProduct> GetProducts(IRequestHandler requestHandler, Catalog catalog);
  }
}