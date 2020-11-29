// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters
{
  public class ProductFilter : IFilter
  {
    public int? Id { get; set; }
    public CategoryFilter Category { get; set; }
    public string Url { get; set; }
    public string Code { get; set; }
    public StringFilter Name { get; set; }
    public StringFilter Description { get; set; }
    public DecimalFilter Price { get; set; }
  }
}