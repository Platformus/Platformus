// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class CredentialFilter : IFilter
  {
    public int? Id { get; set; }
    public UserFilter User { get; set; }
    public CredentialTypeFilter CredentialType { get; set; }
    public StringFilter Identifier { get; set; }
    public string Secret { get; set; }
    public string Extra { get; set; }
  }
}