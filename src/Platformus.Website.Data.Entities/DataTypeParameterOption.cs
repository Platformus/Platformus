// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities
{
  /// <summary>
  /// Represents a data type parameter option.
  /// </summary>
  public class DataTypeParameterOption : IEntity<int>
  {
    /// <summary>
    /// Gets or sets the unique identifier of the data type parameter value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the data type parameter identifier this data type parameter option belongs to.
    /// </summary>
    public int DataTypeParameterId { get; set; }

    /// <summary>
    /// Gets or sets the data type parameter option value.
    /// </summary>
    public string Value { get; set; }

    public virtual DataTypeParameter DataTypeParameter { get; set; }
  }
}
