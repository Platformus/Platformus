// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities;

public class Credential : IEntity<int>
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string? CredentialTypeId { get; set; }
  public string? Identifier { get; set; }
  public string? Secret { get; set; }
  public string? Extra { get; set; }

  public virtual User? User { get; set; }
  public virtual CredentialType? CredentialType { get; set; }
}