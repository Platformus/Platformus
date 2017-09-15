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
    Credential WithKey(int id);
    Credential WithCredentialTypeIdAndIdentifierAndSecret(int credentialTypeId, string identifier, string secret);
    IEnumerable<Credential> FilteredByUserIdRange(int userId, string orderBy, string direction, int skip, int take, string filter);
    void Create(Credential credential);
    void Edit(Credential credential);
    void Delete(int id);
    void Delete(Credential credential);
    int CountByUserId(int userId, string filter);
  }
}