// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Forms.Data.Models
{
  public class CompletedForm : IEntity
  {
    public int Id { get; set; }
    public int FormId { get; set; }
    public DateTime Created { get; set; }

    public virtual Form Form { get; set; }
    public virtual ICollection<CompletedField> CompletedFields { get; set; }
  }
}