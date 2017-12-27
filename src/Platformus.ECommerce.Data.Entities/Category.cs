// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  public class Category : IEntity
  {
    public int Id { get; set; }
    public int? CategoryId { get; set; }
    public int NameId { get; set; }
    public int? Position { get; set; }

    public virtual Category Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
  }
}