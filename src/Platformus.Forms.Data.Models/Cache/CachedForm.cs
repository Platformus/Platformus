// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Forms.Data.Models
{
  public class CachedForm : IEntity
  {
    //[Key]
    //[Required]
    public int CultureId { get; set; }

    //[Key]
    //[Required]
    public int FormId { get; set; }

    //[Required]
    //[StringLength(32)]
    public string Code { get; set; }

    //[Required]
    //[StringLength(64)]
    public string Name { get; set; }
    public string CachedFields { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Form Form { get; set; }
  }
}