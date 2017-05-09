// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Forms.Data.Models
{
  public class SerializedField : IEntity
  {
    public int FieldId { get; set; }
    public string FieldTypeCode { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
    public string SerializedFieldOptions { get; set; }
  }
}