// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class UserFilter : IFilter
{

  public IntegerFilter? Id { get; set; }
  public StringFilter? Name { get; set; }
  public DateTimeFilter? Created { get; set; }
  public EnumerableFilter<UserRoleFilter>? UserRoles { get; set; }
  public EnumerableFilter<CredentialFilter>? Credentials { get; set; }

  public UserFilter() { }

  public UserFilter(IntegerFilter? id = null, StringFilter? name = null, DateTimeFilter? created = null, EnumerableFilter<UserRoleFilter>? userRoles = null, EnumerableFilter<CredentialFilter>? credentials = null)
  {
    this.Id = id;
    this.Name = name;
    this.Created = created;
    this.Credentials = credentials;
    this.UserRoles = userRoles;
  }
}