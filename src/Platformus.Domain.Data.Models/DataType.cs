// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Domain.Data.Models
{
  public class DataType : IEntity
  {
    public int Id { get; set; }
    public string JavaScriptEditorClassName { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }

    public virtual ICollection<Member> Members { get; set; }
  }
}