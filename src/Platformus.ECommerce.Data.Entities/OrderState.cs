// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents an order state.
  /// </summary>
  public class OrderState : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the order state.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the order state. It is set by the user and might be used for the order state retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this order state is related to. It is used to store the localizable order state name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the order state position. Position is used to sort the order states (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Dictionary Name { get; set; }
    public ICollection<Order> Orders { get; set; }
  }
}