// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  public class Catalog : IEntity
  {
    public int Id { get; set; }
    public int? CatalogId { get; set; }
    public int NameId { get; set; }
    public int? Position { get; set; }

    public virtual Catalog Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<Catalog> Catalogs { get; set; }
  }
}