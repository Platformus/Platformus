// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Forms.Data.Entities
{
  public class CompletedField : IEntity
  {
    public int Id { get; set; }
    public int CompletedFormId { get; set; }
    public int FieldId { get; set; }
    public string Value { get; set; }

    public virtual CompletedForm CompletedForm { get; set; }
    public virtual Field Field { get; set; }
  }
}