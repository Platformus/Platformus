// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Globalization.Data.Entities
{
  /// <summary>
  /// Represents a dictionary. The dictionaries are used to group the localizations of all the
  /// localizable properties of the entities.
  /// </summary>
  public class Dictionary : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the dictionary.
    /// </summary>
    public int Id { get; set; }

    public virtual ICollection<Localization> Localizations { get; set; }
  }
}