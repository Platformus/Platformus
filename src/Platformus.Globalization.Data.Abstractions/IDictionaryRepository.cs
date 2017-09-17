// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Dictionary"/> entities.
  /// </summary>
  public interface IDictionaryRepository : IRepository
  {
    /// <summary>
    /// Gets the dictionary by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the dictionary.</param>
    /// <returns>Found dictionary with the given identifier.</returns>
    Dictionary WithKey(int id);

    /// <summary>
    /// Creates the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to create.</param>
    void Create(Dictionary dictionary);

    /// <summary>
    /// Edits the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to edit.</param>
    void Edit(Dictionary dictionary);

    /// <summary>
    /// Deletes the dictionary specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the dictionary to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to delete.</param>
    void Delete(Dictionary dictionary);
  }
}