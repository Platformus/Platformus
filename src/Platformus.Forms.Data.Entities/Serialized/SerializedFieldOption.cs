// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Forms.Data.Entities
{
  public class SerializedFieldOption : IEntity
  {
    public int FieldOptionId { get; set; }
    public string Value { get; set; }
    public int? Position { get; set; }
  }
}