// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.ECommerce.Filters
{
  public class CatalogFilter : IFilter
  {
    public IntegerFilter Id { get; set; }
    public CatalogFilter Owner { get; set; }
    public string Url { get; set; }

    [FilterShortcut("Name.Localizations[]")]
    public LocalizationFilter Name { get; set; }
  }
}