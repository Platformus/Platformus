// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="CredentialType"/> entities.
  /// </summary>
  public interface ICredentialTypeRepository : IRepository
  {
    /// <summary>
    /// Gets the credential type by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credential type.</param>
    /// <returns>Found credential type with the given identifier.</returns>
    CredentialType WithKey(int id);

    /// <summary>
    /// Gets the credential type by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the credential type.</param>
    /// <returns>Found credential type with the given code.</returns>
    CredentialType WithCode(string code);

    /// <summary>
    /// Gets all the credential types using sorting by position (ascending).
    /// </summary>
    /// <returns>Found credential types.</returns>
    IEnumerable<CredentialType> All();
  }
}