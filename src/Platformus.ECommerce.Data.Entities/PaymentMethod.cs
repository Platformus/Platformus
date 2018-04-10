// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents an payment method.
  /// </summary>
  public class PaymentMethod : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the payment method.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the payment method. It is set by the user and might be used for the payment method retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this payment method is related to. It is used to store the localizable payment method name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the payment method position. Position is used to sort the payment methods (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Dictionary Name { get; set; }
    public ICollection<Order> Orders { get; set; }
  }
}