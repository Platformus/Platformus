// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Globalization.Data.Models;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class CachedObject : IEntity
  {
    //[Key]
    //[Required]
    public int CultureId { get; set; }

    //[Key]
    //[Required]
    public int ObjectId { get; set; }

    //[Required]
    public int ClassId { get; set; }

    //[StringLength(32)]
    public string ViewName { get; set; }

    //[StringLength(128)]
    public string Url { get; set; }
    public string CachedProperties { get; set; }
    public string CachedDataSources { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Object Object { get; set; }
  }
}