// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ISerializedFormRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedForm"/> entities in the context of SQL Server database.
  /// </summary>
  public class SerializedFormRepository : RepositoryBase<SerializedForm>, ISerializedFormRepository
  {
    /// <summary>
    /// Gets the serialized form by the culture identifier and form identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized form belongs to.</param>
    /// <param name="formId">The unique identifier of the form this serialized form belongs to.</param>
    /// <returns>Found serialized form with the given culture identifier and form identifier.</returns>
    public SerializedForm WithKey(int cultureId, int formId)
    {
      return this.dbSet.Find(cultureId, formId);
    }

    /// <summary>
    /// Gets the serialized form by the culture identifier and form code (case insensitive).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized form belongs to.</param>
    /// <param name="code">The unique code of the form this serialized form belongs to.</param>
    /// <returns>Found serialized form with the given culture identifier and form code.</returns>
    public SerializedForm WithCultureIdAndCode(int cultureId, string code)
    {
      return this.dbSet.FirstOrDefault(sf => sf.CultureId == cultureId && string.Equals(sf.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Creates the serialized form.
    /// </summary>
    /// <param name="serializedForm">The serialized form to create.</param>
    public void Create(SerializedForm serializedForm)
    {
      this.dbSet.Add(serializedForm);
    }

    /// <summary>
    /// Edits the serialized form.
    /// </summary>
    /// <param name="serializedForm">The serialized form to edit.</param>
    public void Edit(SerializedForm serializedForm)
    {
      this.storageContext.Entry(serializedForm).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the serialized form specified by the culture identifier and form identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized form belongs to.</param>
    /// <param name="formId">The unique identifier of the form this serialized form belongs to.</param>
    public void Delete(int cultureId, int formId)
    {
      this.Delete(this.WithKey(cultureId, formId));
    }

    /// <summary>
    /// Deletes the serialized form.
    /// </summary>
    /// <param name="serializedForm">The serialized form to delete.</param>
    public void Delete(SerializedForm serializedForm)
    {
      this.dbSet.Remove(serializedForm);
    }
  }
}