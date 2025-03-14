// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class FieldFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public FormFilter? Form { get; set; }
  public StringFilter? Name { get; set; }

  public FieldFilter() { }

  public FieldFilter(IntegerFilter? id = null, FormFilter? form = null, StringFilter? name = null)
  {
    this.Id = id;
    this.Form = form;
    this.Name = name;
  }
}