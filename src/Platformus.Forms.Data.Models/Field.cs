// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Forms.Data.Models
{
  public class Field : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int FormId { get; set; }

    //[Required]
    public int FieldTypeId { get; set; }

    //[Required]
    public int NameId { get; set; }
    public int? Position { get; set; }

    public virtual Form Form { get; set; }
    public virtual FieldType FieldType { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<FieldOption> FieldOptions { get; set; }
  }
}