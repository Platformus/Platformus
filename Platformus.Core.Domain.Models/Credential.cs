// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class Credential : IModel<Data.Entities.Credential, CredentialFilter>
{
  public int Id { get; set; }
  public User? User { get; set; }
  public CredentialType? CredentialType { get; set; }
  public string? Identifier { get; set; }
  public string? Secret { get; set; }
  public string? Extra { get; set; }

  public Credential() { }

  public Credential(Data.Entities.Credential _credential) : this(_credential, mapUser: true) { }

  public Credential(Data.Entities.Credential _credential, bool mapUser = true, bool mapCredentialType = true)
  {
    this.Id = _credential.Id;
    this.User = mapUser ? _credential.User == null ? new User { Id = _credential.UserId } : new User(_credential.User, mapCredentials: false) : null;
    this.CredentialType = mapCredentialType ? _credential.CredentialType == null ? new CredentialType { Id = _credential.CredentialTypeId } : new CredentialType(_credential.CredentialType, mapCredentials: false) : null;
    this.Identifier = _credential.Identifier;
    this.Secret = _credential.Secret;
    this.Extra = _credential.Extra;
  }

  public Data.Entities.Credential ToEntity()
  {
    return new Data.Entities.Credential()
    {
      Id = this.Id,
      UserId = this.User?.Id ?? 0,
      CredentialTypeId = this.CredentialType?.Id,
      Identifier = this.Identifier,
      Secret = this.Secret,
      Extra = this.Extra
    };
  }
}