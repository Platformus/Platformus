// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  public class Product : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public int NameId { get; set; }

    public virtual Dictionary Name { get; set; }
  }
}