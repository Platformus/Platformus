// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Culture"/> entities.
  /// </summary>
  public interface ICultureRepository : IRepository
  {
    /// <summary>
    /// Gets the culture by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the culture.</param>
    /// <returns>Found culture with the given identifier.</returns>
    Culture WithKey(int id);

    /// <summary>
    /// Gets the culture by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the culture.</param>
    /// <returns>Found culture with the given code.</returns>
    Culture WithCode(string code);

    /// <summary>
    /// Gets the neutral culture.
    /// </summary>
    /// <returns>Found neutral culture.</returns>
    Culture Neutral();

    /// <summary>
    /// Gets the default culture.
    /// </summary>
    /// <returns>Found default culture.</returns>
    Culture Default();

    /// <summary>
    /// Gets all the cultures using sorting by name (ascending).
    /// </summary>
    /// <returns>Found cultures.</returns>
    IEnumerable<Culture> All();

    /// <summary>
    /// Gets the not neutral cultures using sorting by name (ascending).
    /// </summary>
    /// <returns>Found not neutral cultures.</returns>
    IEnumerable<Culture> NotNeutral();

    /// <summary>
    /// Gets all the cultures using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The culture property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of cultures that should be skipped.</param>
    /// <param name="take">The number of cultures that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found cultures using the given filtering, sorting, and paging.</returns>
    IEnumerable<Culture> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the culture.
    /// </summary>
    /// <param name="culture">The culture to create.</param>
    void Create(Culture culture);

    /// <summary>
    /// Edits the culture.
    /// </summary>
    /// <param name="culture">The culture to edit.</param>
    void Edit(Culture culture);

    /// <summary>
    /// Deletes the culture specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the culture to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the culture.
    /// </summary>
    /// <param name="culture">The culture to delete.</param>
    void Delete(Culture culture);

    /// <summary>
    /// Counts the number of the cultures with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of cultures found.</returns>
    int Count(string filter);
  }
}