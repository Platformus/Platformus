// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters
{
  public class CartFilter : IFilter
  {
    public int? Id { get; set; }
    public Guid? ClientSideId { get; set; }
    public DateTimeFilter Created { get; set; }

    public CartFilter() { }

    public CartFilter(int? id = null, Guid? clientSideId = null, DateTimeFilter created = null)
    {
      Id = id;
      ClientSideId = clientSideId;
      Created = created;
    }
  }
}