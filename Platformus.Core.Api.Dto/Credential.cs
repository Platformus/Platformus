// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto;

[AuthenticatedOnly]
public class Credential : IDto<Domain.Models.Credential>
{
  public int Id { get; set; }
  public User? User { get; set; }
  public CredentialType? CredentialType { get; set; }
  public string? Identifier { get; set; }
  public string? Secret { get; set; }
  public string? Extra { get; set; }

  public Credential() { }

  public Credential(Domain.Models.Credential _credential)
  {
    this.Id = _credential.Id;
    this.User = _credential.User == null ? null : new User(_credential.User);
    this.CredentialType = _credential.CredentialType == null ? null : new CredentialType(_credential.CredentialType);
    this.Identifier = _credential.Identifier;
  }

  public Domain.Models.Credential ToModel()
  {
    return new Domain.Models.Credential()
    {
      Id = this.Id,
      User = this.User?.ToModel(),
      CredentialType = this.CredentialType?.ToModel(),
      Identifier = this.Identifier,
      Secret = this.Secret,
      Extra = this.Extra
    };
  }
}