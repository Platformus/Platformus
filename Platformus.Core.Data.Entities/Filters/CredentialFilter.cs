// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class CredentialFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public UserFilter? User { get; set; }
  public CredentialTypeFilter? CredentialType { get; set; }
  public StringFilter? Identifier { get; set; }
  public StringFilter? Secret { get; set; }
  public StringFilter? Extra { get; set; }

  public CredentialFilter() { }

  public CredentialFilter(IntegerFilter? id = null, UserFilter? user = null, CredentialTypeFilter? credentialType = null, StringFilter? identifier = null, StringFilter? secret = null, StringFilter? extra = null)
  {
    this.Id = id;
    this.User = user;
    this.CredentialType = credentialType;
    this.Identifier = identifier;
    this.Secret = secret;
    this.Extra = extra;
  }
}