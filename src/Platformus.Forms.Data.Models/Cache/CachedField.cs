// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Forms.Data.Models
{
  public class CachedField : IEntity
  {
    public int FieldId { get; set; }
    public string FieldTypeCode { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
    public string CachedFieldOptions { get; set; }
  }
}