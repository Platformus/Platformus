// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class DataType : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    //[StringLength(128)]
    public string JavaScriptEditorClassName { get; set; }

    //[Required]
    //[StringLength(64)]
    public string Name { get; set; }
    public int? Position { get; set; }

    public virtual ICollection<Member> Members { get; set; }
  }
}