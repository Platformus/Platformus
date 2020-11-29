// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters
{
  public class CategoryFilter : IFilter
  {
    public IntegerFilter Id { get; set; }
    public CategoryFilter Owner { get; set; }
    public StringFilter Name { get; set; }
  }
}