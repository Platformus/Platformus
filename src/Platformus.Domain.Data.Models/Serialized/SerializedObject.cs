// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain.Data.Models
{
  public class SerializedObject : IEntity
  {
    public int CultureId { get; set; }
    public int ObjectId { get; set; }
    public string UrlPropertyStringValue { get; set; }
    public string SerializedProperties { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Object Object { get; set; }
  }
}