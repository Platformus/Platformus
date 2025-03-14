// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class CredentialType : IModel<Data.Entities.CredentialType, CredentialTypeFilter>
{
  public string? Id { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<Credential>? Credentials { get; set; }

  public CredentialType() { }

  public CredentialType(Data.Entities.CredentialType _credentialType) : this(_credentialType, mapCredentials: true) { }

  public CredentialType(Data.Entities.CredentialType _credentialType, bool mapCredentials = true)
  {
    this.Id = _credentialType.Id;
    this.Name = _credentialType.Name;
    this.Position = _credentialType.Position;
    this.Credentials = mapCredentials ? _credentialType.Credentials?.Select(c => new Credential(c, mapCredentialType: false)).ToList() : null;
  }

  public Data.Entities.CredentialType ToEntity()
  {
    return new Data.Entities.CredentialType()
    {
      Id = this.Id,
      Name = this.Name,
      Position = this.Position
    };
  }
}