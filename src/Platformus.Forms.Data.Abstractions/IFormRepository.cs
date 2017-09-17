// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Form"/> entities.
  /// </summary>
  public interface IFormRepository : IRepository
  {
    /// <summary>
    /// Gets the form by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the form.</param>
    /// <returns>Found form with the given identifier.</returns>
    Form WithKey(int id);

    /// <summary>
    /// Gets the form by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the form.</param>
    /// <returns>Found form with the given code.</returns>
    Form WithCode(string code);

    /// <summary>
    /// Gets all the forms using sorting by name (ascending).
    /// </summary>
    /// <returns>Found forms.</returns>
    IEnumerable<Form> All();

    /// <summary>
    /// Creates the form.
    /// </summary>
    /// <param name="form">The form to create.</param>
    void Create(Form form);

    /// <summary>
    /// Edits the form.
    /// </summary>
    /// <param name="form">The form to edit.</param>
    void Edit(Form form);

    /// <summary>
    /// Deletes the form specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the form to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the form.
    /// </summary>
    /// <param name="form">The form to delete.</param>
    void Delete(Form form);
  }
}