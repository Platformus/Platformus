// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a field type. The field types are used to specify how the field should look and behave,
  /// and how it should be processed.
  /// </summary>
  public class FieldType : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
  }
}