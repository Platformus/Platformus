// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a category.
  /// </summary>
  public class Category : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the category identifier this category belongs to (as a parent category).
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this category is related to. It is used to store the localizable category name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the category position. Position is used to sort the categories inside the parent category (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Category Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
  }
}