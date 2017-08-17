// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a field option. The field options are used to build the fields.
  /// </summary>
  public class FieldOption : IEntity
  {
    public int Id { get; set; }
    public int FieldId { get; set; }
    public int ValueId { get; set; }
    public int? Position { get; set; }

    public virtual Field Field { get; set; }
    public virtual Dictionary Value { get; set; }
  }
}