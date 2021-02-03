// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities
{
  /// <summary>
  /// Represents a localization. The localizations are used to store the localized text value for the given
  /// dictionary and culture.
  /// </summary>
  public class Localization : IEntity<int>
  {
    /// <summary>
    /// Gets or sets the unique identifier of the localization.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this localization belongs to.
    /// </summary>
    public int DictionaryId { get; set; }

    /// <summary>
    /// Gets or sets the culture identifier this localization belongs to.
    /// </summary>
    public string CultureId { get; set; }

    /// <summary>
    /// Gets or sets the localization value.
    /// </summary>
    public string Value { get; set; }

    public virtual Dictionary Dictionary { get; set; }
    public virtual Culture Culture { get; set; }
  }
}