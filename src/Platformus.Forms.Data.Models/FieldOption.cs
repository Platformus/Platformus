// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Forms.Data.Models
{
  public class FieldOption : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int FieldId { get; set; }

    //[Required]
    public int ValueId { get; set; }
    public int? Position { get; set; }

    public virtual Field Field { get; set; }
    public virtual Dictionary Value { get; set; }
  }
}