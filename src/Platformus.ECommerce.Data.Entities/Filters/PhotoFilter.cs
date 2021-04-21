// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters
{
  public class PhotoFilter : IFilter
  {
    public int? Id { get; set; }
    public ProductFilter Product { get; set; }
    public bool? IsCover { get; set; }

    public PhotoFilter() { }

    public PhotoFilter(int? id = null, ProductFilter product = null, bool? isCover = null)
    {
      Id = id;
      Product = product;
      IsCover = isCover;
    }
  }
}