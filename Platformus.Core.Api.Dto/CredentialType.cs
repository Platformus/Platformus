// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/credential-types", Magicalizer.Api.Dto.Abstractions.HttpMethod.Get)]
[AuthenticatedOnly]
public class CredentialType : IDto<Domain.Models.CredentialType>
{
  public string? Id { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<Credential>? Credentials { get; set; }

  public CredentialType() { }

  public CredentialType(Domain.Models.CredentialType _credentialType)
  {
    this.Id = _credentialType.Id;
    this.Name = _credentialType.Name;
    this.Position = _credentialType.Position;
    this.Credentials = _credentialType.Credentials?.Select(c => new Credential(c)).ToList();
  }

  public Domain.Models.CredentialType ToModel()
  {
    return new Domain.Models.CredentialType()
    {
      Id = this.Id,
      Name = this.Name,
      Position = this.Position
    };
  }
}