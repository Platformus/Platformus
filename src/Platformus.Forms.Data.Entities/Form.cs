// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  public class Form : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public int NameId { get; set; }
    public string Email { get; set; }
    public string RedirectUrl { get; set; }

    public virtual Dictionary Name { get; set; }
    public virtual ICollection<Field> Fields { get; set; }
  }
}