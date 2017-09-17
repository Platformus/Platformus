// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Localization"/> entities.
  /// </summary>
  public interface ILocalizationRepository : IRepository
  {
    /// <summary>
    /// Gets the localization by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the localization.</param>
    /// <returns>Found localization with the given identifier.</returns>
    Localization WithKey(int id);

    /// <summary>
    /// Gets the localization by the dictionary identifier and culture identifier.
    /// </summary>
    /// <param name="dictionaryId">The unique identifier of the dictionary this localization belongs to.</param>
    /// <param name="cultureId">The unique identifier of the culture this localization belongs to.</param>
    /// <returns>Found localization with the given dictionary identifier and culture identifier.</returns>
    Localization WithDictionaryIdAndCultureId(int dictionaryId, int cultureId);

    /// <summary>
    /// Gets the localizations filtered by the dictionary identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="dictionaryId">The unique identifier of the dictionary these localizations belongs to.</param>
    /// <returns>Found objects.</returns>
    IEnumerable<Localization> FilteredByDictionaryId(int dictionaryId);

    /// <summary>
    /// Creates the localization.
    /// </summary>
    /// <param name="localization">The localization to create.</param>
    void Create(Localization localization);

    /// <summary>
    /// Edits the localization.
    /// </summary>
    /// <param name="localization">The localization to edit.</param>
    void Edit(Localization localization);

    /// <summary>
    /// Deletes the localization specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the localization to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the localization.
    /// </summary>
    /// <param name="localization">The localization to delete.</param>
    void Delete(Localization localization);
  }
}