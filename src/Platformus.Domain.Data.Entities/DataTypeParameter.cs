// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a data type parameter. The data type parameters are used to pass the parameters to the
  /// object property value editors (for example, maximum text length for a text box).
  /// </summary>
  public class DataTypeParameter : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the data type parameter.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the data type identifier this data type parameter belongs to.
    /// </summary>
    public int DataTypeId { get; set; }

    /// <summary>
    /// Gets or sets the JavaScript editor class name (name of the JavaScript function that is used to build the editor).
    /// </summary>
    public string JavaScriptEditorClassName { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the data type parameter. It is set by the user and might be used for the data type parameter retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the data type parameter name.
    /// </summary>
    public string Name { get; set; }

    public virtual DataType DataType { get; set; }
    public virtual ICollection<DataTypeParameterValue> DataTypeParameterValues { get; set; }
  }
}