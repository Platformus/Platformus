// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="ICredentialRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Credential"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class CredentialRepository : RepositoryBase<Credential>, ICredentialRepository
  {
    /// <summary>
    /// Gets the credential by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credential.</param>
    /// <returns>Found credential with the given identifier.</returns>
    public Credential WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the credential by the credential type identifier and user identifier.
    /// </summary>
    /// <param name="credentialTypeId">The unique identifier of the credential type this credential belongs to.</param>
    /// <param name="identifier">The identifier of the user.</param>
    /// <returns>Found credential with the given credential type identifier and user identifier.</returns>
    public Credential WithCredentialTypeIdAndIdentifier(int credentialTypeId, string identifier)
    {
      return this.dbSet.FirstOrDefault(c => c.CredentialTypeId == credentialTypeId && string.Equals(c.Identifier, identifier, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the credentials filtered by the user identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="userId">The unique identifier of the user these credentials belongs to.</param>
    /// <param name="orderBy">The credential property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of credentials that should be skipped.</param>
    /// <param name="take">The number of credentials that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found credentials filtered by the user identifier using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Credential> FilteredByUserIdRange(int userId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredCredentials(dbSet.AsNoTracking(), userId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the credential.
    /// </summary>
    /// <param name="credential">The credential to create.</param>
    public void Create(Credential credential)
    {
      this.dbSet.Add(credential);
    }

    /// <summary>
    /// Edits the credential.
    /// </summary>
    /// <param name="credential">The credential to edit.</param>
    public void Edit(Credential credential)
    {
      this.storageContext.Entry(credential).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the credential specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credential to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the credential.
    /// </summary>
    /// <param name="credential">The credential to delete.</param>
    public void Delete(Credential credential)
    {
      this.dbSet.Remove(credential);
    }

    /// <summary>
    /// Counts the number of the credentials filtered by the user identifier with the given filtering.
    /// </summary>
    /// <param name="userId">The unique identifier of the user these credentials belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of credentials found.</returns>
    public int CountByUserId(int userId, string filter)
    {
      return this.GetFilteredCredentials(dbSet, userId, filter).Count();
    }

    private IQueryable<Credential> GetFilteredCredentials(IQueryable<Credential> credentials, int userId, string filter)
    {
      credentials = credentials.Where(c => c.UserId == userId);

      if (string.IsNullOrEmpty(filter))
        return credentials;

      return credentials.Where(c => c.Identifier.ToLower().Contains(filter.ToLower()));
    }
  }
}