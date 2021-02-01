// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a cart.
  /// </summary>
  public class Cart : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique client-side identifier of the cart.
    /// </summary>
    public Guid ClientSideId { get; set; }

    /// <summary>
    /// Gets or sets the date and time this cart is created at.
    /// </summary>
    public DateTime Created { get; set; }

    public virtual ICollection<Position> Positions { get; set; }
  }
}