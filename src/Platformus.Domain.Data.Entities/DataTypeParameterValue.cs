// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a data type parameter value. The data type parameter values are used to store the data
  /// for the data type parameters.
  /// </summary>
  public class DataTypeParameterValue : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the data type parameter value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the data type parameter identifier this data type parameter value belongs to.
    /// </summary>
    public int DataTypeParameterId { get; set; }

    /// <summary>
    /// Gets or sets the member identifier this data type parameter value is related to.
    /// </summary>
    public int MemberId { get; set; }

    /// <summary>
    /// Gets or sets the data type parameter value value.
    /// </summary>
    public string Value { get; set; }

    public virtual DataTypeParameter DataTypeParameter { get; set; }
    public virtual Member Member { get; set; }
  }
}
