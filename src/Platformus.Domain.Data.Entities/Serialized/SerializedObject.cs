// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// The serialized object contains the object and corresponding properties data inside the single object.
  /// This reduces the number of storage read operations while object rendering.
  /// </summary>
  public class SerializedObject : IEntity
  {
    public int CultureId { get; set; }
    public int ObjectId { get; set; }
    public int ClassId { get; set; }
    public string UrlPropertyStringValue { get; set; }
    public string SerializedProperties { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Object Object { get; set; }
    public virtual Class Class { get; set; }
  }
}