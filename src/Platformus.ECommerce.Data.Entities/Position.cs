// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a position.
  /// </summary>
  public class Position : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the position.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the cart identifier this position belongs to.
    /// </summary>
    public int? CartId { get; set; }

    /// <summary>
    /// Gets or sets the order identifier this position belongs to.
    /// </summary>
    public int? OrderId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier this position is related to.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product price at the moment of order.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the product quantity.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Gets or sets the position subtotal (price multiplied by quantity).
    /// </summary>
    public decimal Subtotal { get; set; }

    public virtual Cart Cart { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
  }
}