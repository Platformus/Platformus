// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities
{
  /// <summary>
  /// Represents a model state. It is used to keep the form field values during redirects because of the validation errors.
  /// </summary>
  public class ModelState : IEntity<Guid>
  {
    /// <summary>
    /// Gets or sets the unique client-side identifier of the model state.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the serialized ModelState object.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the date and time this model state is created at.
    /// </summary>
    public DateTime Created { get; set; }
  }
}