// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class CredentialTypeFilter : IFilter
{
  public StringFilter? Id { get; set; }
  public StringFilter? Name { get; set; }

  public CredentialTypeFilter() { }

  public CredentialTypeFilter(StringFilter? id = null, StringFilter? name = null)
  {
    this.Id = id;
    this.Name = name;
  }
}