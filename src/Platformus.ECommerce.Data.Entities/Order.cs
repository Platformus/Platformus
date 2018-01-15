// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents an order.
  /// </summary>
  public class Order : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time this order is created at.
    /// </summary>
    public DateTime Created { get; set; }

    public virtual ICollection<Position> Positions { get; set; }
  }
}