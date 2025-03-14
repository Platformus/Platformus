// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities;

public class CredentialType : IEntity<string>
{
  public string? Id { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }

  public virtual ICollection<Credential>? Credentials { get; set; }
}