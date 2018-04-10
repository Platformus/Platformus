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
    /// Gets or sets the order state identifier this order belongs to.
    /// </summary>
    public int OrderStateId { get; set; }

    /// <summary>
    /// Gets or sets the payment method identifier this order belongs to.
    /// </summary>
    public int PaymentMethodId { get; set; }

    /// <summary>
    /// Gets or sets the delivery method identifier this order belongs to.
    /// </summary>
    public int DeliveryMethodId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string CustomerFirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string CustomerLastName { get; set; }

    /// <summary>
    /// Gets or sets the phone of the customer.
    /// </summary>
    public string CustomerPhone { get; set; }

    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public string CustomerAddress { get; set; }

    /// <summary>
    /// Gets or sets the note to the order.
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// Gets or sets the date and time this order is created at.
    /// </summary>
    public DateTime Created { get; set; }

    public virtual OrderState OrderState { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }
    public virtual DeliveryMethod DeliveryMethod { get; set; }
    public virtual ICollection<Cart> Carts { get; set; }
  }
}