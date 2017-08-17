// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a data type parameter. The data type parameters are used to pass the parameters to the
  /// object property editors (for example, maximum text length).
  /// </summary>
  public class DataTypeParameter : IEntity
  {
    public int Id { get; set; }
    public int DataTypeId { get; set; }
    public string JavaScriptEditorClassName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public virtual DataType DataType { get; set; }
    public virtual ICollection<DataTypeParameterValue> DataTypeParameterValues { get; set; }
  }
}