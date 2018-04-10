// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a catalog.
  /// </summary>
  public class Catalog : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the catalog.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the catalog identifier this catalog belongs to (as a parent catalog).
    /// </summary>
    public int? CatalogId { get; set; }

    /// <summary>
    /// Gets or sets the unique URL of the catalog.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this catalog is related to. It is used to store the localizable catalog name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the name (including namespace) of the C# class which will be instantiated each time
    /// when the catalog content is requested by a user.
    /// </summary>
    public string CSharpClassName { get; set; }

    /// <summary>
    /// Gets or sets the parameters (key=value pairs separated by commas) for the C# class instances.
    /// </summary>
    public string Parameters { get; set; }

    /// <summary>
    /// Gets or sets the catalog position. Position is used to sort the catalogs inside the parent catalog (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Catalog Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<Catalog> Catalogs { get; set; }
  }
}