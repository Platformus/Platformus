// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents an object property. The properties are used to store the object data using the different data types.
  /// </summary>
  public class Property : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the property.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the object identifier this property belongs to.
    /// </summary>
    public int ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the member identifier this property is related to.
    /// </summary>
    public int MemberId { get; set; }

    /// <summary>
    /// Gets or sets the property integer value.
    /// </summary>
    public int? IntegerValue { get; set; }

    /// <summary>
    /// Gets or sets the property decimal value.
    /// </summary>
    public decimal? DecimalValue { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this property is related to.
    /// It is used to store the property (optionally localizable) string value.
    /// </summary>
    public int? StringValueId { get; set; }

    /// <summary>
    /// Gets or sets the property <see cref="DateTime"/> value.
    /// </summary>
    public DateTime? DateTimeValue { get; set; }

    public virtual Object Object { get; set; }
    public virtual Member Member { get; set; }
    public virtual Dictionary StringValue { get; set; }
  }
}