// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a many-to-many relationship between the products and attributes.
  /// </summary>
  public class ProductAttribute : IEntity
  {
    /// <summary>
    /// Gets or sets the product identifier this product attribute is related to.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the attribute identifier this product attribute is related to.
    /// </summary>
    public int AttributeId { get; set; }

    public virtual Product Product { get; set; }
    public virtual Attribute Attribute { get; set; }
  }
}