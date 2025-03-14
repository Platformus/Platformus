// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class User : IModel<Data.Entities.User, UserFilter>
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public DateTime Created { get; set; }
  public IEnumerable<Credential>? Credentials { get; set; }
  public IEnumerable<UserRole>? UserRoles { get; set; }

  public User() { }

  public User(Data.Entities.User _user) : this(_user, mapCredentials: true, mapUserRoles: true) { }

  public User(Data.Entities.User _user, bool mapCredentials = true, bool mapUserRoles = true)
  {
    this.Id = _user.Id;
    this.Name = _user.Name;
    this.Created = _user.Created;
    this.Credentials = mapCredentials ? _user.Credentials?.Select(c => new Credential(c, mapUser: false)).ToList() : null;
    this.UserRoles = mapUserRoles ? _user.UserRoles?.Select(ur => new UserRole(ur, mapUser: false)).ToList() : null;
  }

  public Data.Entities.User ToEntity()
  {
    return new Data.Entities.User()
    {
      Id = this.Id,
      Name = this.Name,
      Created = this.Created
    };
  }
}