// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Class"/> entities.
  /// </summary>
  public interface IClassRepository : IRepository
  {
    /// <summary>
    /// Gets the class by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the class.</param>
    /// <returns>Found class with the given identifier.</returns>
    Class WithKey(int id);

    /// <summary>
    /// Gets the class by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the class.</param>
    /// <returns>Found class with the given code.</returns>
    Class WithCode(string code);

    /// <summary>
    /// Gets all the classes using sorting by name (ascending).
    /// </summary>
    /// <returns>Found classes.</returns>
    IEnumerable<Class> All();

    /// <summary>
    /// Gets all the classes using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The class property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of classes that should be skipped.</param>
    /// <param name="take">The number of classes that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found classes using the given filtering, sorting, and paging.</returns>
    IEnumerable<Class> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Gets the classes filtered by the parent class identifier using sorting by name (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the parent class these classes belongs to.</param>
    /// <returns>Found classes.</returns>
    IEnumerable<Class> FilteredByClassId(int? classId);

    /// <summary>
    /// Gets the abstract classes using sorting by name (ascending).
    /// </summary>
    /// <returns>Found abstract classes.</returns>
    IEnumerable<Class> Abstract();

    /// <summary>
    /// Gets the not abstract classes using sorting by name (ascending).
    /// </summary>
    /// <returns>Found not abstract classes.</returns>
    IEnumerable<Class> NotAbstract();

    /// <summary>
    /// Creates the class.
    /// </summary>
    /// <param name="@class">The class to create.</param>
    void Create(Class @class);

    /// <summary>
    /// Edits the class.
    /// </summary>
    /// <param name="@class">The class to edit.</param>
    void Edit(Class @class);

    /// <summary>
    /// Deletes the class specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the class to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the class.
    /// </summary>
    /// <param name="@class">The class to delete.</param>
    void Delete(Class @class);

    /// <summary>
    /// Counts the number of the classes with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of classes found.</returns>
    int Count(string filter);
  }
}