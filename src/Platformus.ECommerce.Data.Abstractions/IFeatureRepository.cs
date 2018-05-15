// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Feature"/> entities.
  /// </summary>
  public interface IFeatureRepository : IRepository
  {
    /// <summary>
    /// Gets the feature by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the feature.</param>
    /// <returns>Found feature with the given identifier.</returns>
    Feature WithKey(int id);

    /// <summary>
    /// Gets the feature by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the feature.</param>
    /// <returns>Found feature with the given code.</returns>
    Feature WithCode(string code);

    /// <summary>
    /// Gets all the features using sorting by position (ascending).
    /// </summary>
    /// <returns>Found features.</returns>
    IEnumerable<Feature> All();

    /// <summary>
    /// Gets all the features using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The feature property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of features that should be skipped.</param>
    /// <param name="take">The number of features that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found features using the given filtering, sorting, and paging.</returns>
    IEnumerable<Feature> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the feature.
    /// </summary>
    /// <param name="feature">The feature to create.</param>
    void Create(Feature feature);

    /// <summary>
    /// Edits the feature.
    /// </summary>
    /// <param name="feature">The feature to edit.</param>
    void Edit(Feature feature);

    /// <summary>
    /// Deletes the feature specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the feature to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the feature.
    /// </summary>
    /// <param name="feature">The feature to delete.</param>
    void Delete(Feature feature);

    /// <summary>
    /// Counts the number of the features with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of features found.</returns>
    int Count(string filter);
  }
}