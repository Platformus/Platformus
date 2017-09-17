// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedForm"/> entities.
  /// </summary>
  public interface ISerializedFormRepository : IRepository
  {
    /// <summary>
    /// Gets the serialized form by the culture identifier and form identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized form belongs to.</param>
    /// <param name="formId">The unique identifier of the form this serialized form belongs to.</param>
    /// <returns>Found serialized form with the given culture identifier and form identifier.</returns>
    SerializedForm WithKey(int cultureId, int formId);

    /// <summary>
    /// Gets the serialized form by the culture identifier and form code (case insensitive).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized form belongs to.</param>
    /// <param name="code">The unique code of the form this serialized form belongs to.</param>
    /// <returns>Found serialized form with the given culture identifier and form code.</returns>
    SerializedForm WithCultureIdAndCode(int cultureId, string code);

    /// <summary>
    /// Creates the serialized form.
    /// </summary>
    /// <param name="serializedForm">The serialized form to create.</param>
    void Create(SerializedForm serializedForm);

    /// <summary>
    /// Edits the serialized form.
    /// </summary>
    /// <param name="serializedForm">The serialized form to edit.</param>
    void Edit(SerializedForm serializedForm);

    /// <summary>
    /// Deletes the serialized form specified by the culture identifier and form identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized form belongs to.</param>
    /// <param name="formId">The unique identifier of the form this serialized form belongs to.</param>
    void Delete(int cultureId, int formId);

    /// <summary>
    /// Deletes the serialized form.
    /// </summary>
    /// <param name="serializedForm">The serialized form to delete.</param>
    void Delete(SerializedForm serializedForm);
  }
}