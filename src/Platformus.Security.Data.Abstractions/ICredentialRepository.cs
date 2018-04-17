// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Credential"/> entities.
  /// </summary>
  public interface ICredentialRepository : IRepository
  {
    /// <summary>
    /// Gets the credential by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credential.</param>
    /// <returns>Found credential with the given identifier.</returns>
    Credential WithKey(int id);

    /// <summary>
    /// Gets the credential by the credential type identifier and user identifier.
    /// </summary>
    /// <param name="credentialTypeId">The unique identifier of the credential type this credential belongs to.</param>
    /// <param name="identifier">The identifier of the user.</param>
    /// <returns>Found credential with the given credential type identifier and user identifier.</returns>
    Credential WithCredentialTypeIdAndIdentifier(int credentialTypeId, string identifier);

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
    IEnumerable<Credential> FilteredByUserIdRange(int userId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the credential.
    /// </summary>
    /// <param name="credential">The credential to create.</param>
    void Create(Credential credential);

    /// <summary>
    /// Edits the credential.
    /// </summary>
    /// <param name="credential">The credential to edit.</param>
    void Edit(Credential credential);

    /// <summary>
    /// Deletes the credential specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credential to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the credential.
    /// </summary>
    /// <param name="credential">The credential to delete.</param>
    void Delete(Credential credential);

    /// <summary>
    /// Counts the number of the credentials filtered by the user identifier with the given filtering.
    /// </summary>
    /// <param name="userId">The unique identifier of the user these credentials belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of credentials found.</returns>
    int CountByUserId(int userId, string filter);
  }
}