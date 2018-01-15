// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

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
    /// Gets or sets the order identifier this position belongs to.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier this position is related to.
    /// </summary>
    public int ProductId { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
  }
}