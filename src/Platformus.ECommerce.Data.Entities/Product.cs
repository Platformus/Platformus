// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a product.
  /// </summary>
  public class Product : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the category identifier this product belongs to.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the unique URL of the product.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the product. It is set by the user and might be used for the product retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this product is related to. It is used to store the localizable product name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this product is related to. It is used to store the localizable product description.
    /// </summary>
    public int DescriptionId { get; set; }

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this product is related to. It is used to store the localizable product title.
    /// </summary>
    public int TitleId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this product is related to. It is used to store the localizable product META description.
    /// </summary>
    public int MetaDescriptionId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this product is related to. It is used to store the localizable product META keywords.
    /// </summary>
    public int MetaKeywordsId { get; set; }

    public virtual Category Category { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual Dictionary Description { get; set; }
    public virtual Dictionary Title { get; set; }
    public virtual Dictionary MetaDescription { get; set; }
    public virtual Dictionary MetaKeywords { get; set; }
    public virtual ICollection<Photo> Photos { get; set; }
  }
}