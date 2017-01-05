// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain.Data.Models
{
  public class CachedObject : IEntity
  {
    public int CultureId { get; set; }
    public int ObjectId { get; set; }
    public int ClassId { get; set; }
    public string ViewName { get; set; }
    public string Url { get; set; }
    public string CachedProperties { get; set; }
    public string CachedDataSources { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Object Object { get; set; }
  }
}