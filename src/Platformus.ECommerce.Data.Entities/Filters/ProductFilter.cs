// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.ECommerce.Filters
{
  public class ProductFilter : IFilter
  {
    public int? Id { get; set; }
    public CategoryFilter Category { get; set; }
    public string Url { get; set; }
    public string Code { get; set; }

    [FilterShortcut("Name.Localizations[]")]
    public LocalizationFilter Name { get; set; }

    [FilterShortcut("Description.Localizations[]")]
    public LocalizationFilter Description { get; set; }
    public DecimalFilter Price { get; set; }
  }
}