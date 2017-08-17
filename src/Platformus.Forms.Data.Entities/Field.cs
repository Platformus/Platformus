// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a field. The fields are used to build the forms.
  /// </summary>
  public class Field : IEntity
  {
    public int Id { get; set; }
    public int FormId { get; set; }
    public int FieldTypeId { get; set; }
    public int NameId { get; set; }
    public bool IsRequired { get; set; }
    public int? MaxLength { get; set; }
    public int? Position { get; set; }

    public virtual Form Form { get; set; }
    public virtual FieldType FieldType { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<FieldOption> FieldOptions { get; set; }
  }
}