// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class RefreshTokenFilter : IFilter
{
  public StringFilter? Id { get; set; }
  public UserFilter? User { get; set; }

  public RefreshTokenFilter() { }

  public RefreshTokenFilter(StringFilter? id = null, UserFilter? user = null)
  {
    this.Id = id;
    this.User = user;
  }
}