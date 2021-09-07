// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class ModelStateFilter : IFilter
  {
    public Guid? Id { get; set; }
    public DateTimeFilter Created { get; set; }

    public ModelStateFilter() { }

    public ModelStateFilter(Guid? id = null, DateTimeFilter created = null)
    {
      Id = id;
      Created = created;
    }
  }
}