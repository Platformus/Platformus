// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class CompletedFormFilter : IFilter
{
  public int? Id { get; set; }
  public FormFilter Form { get; set; }
  public DateTimeFilter Created { get; set; }

  public CompletedFormFilter() { }

  public CompletedFormFilter(int? id = null, FormFilter form = null, DateTimeFilter created = null)
  {
    Id = id;
    Form = form;
    Created = created;
  }
}