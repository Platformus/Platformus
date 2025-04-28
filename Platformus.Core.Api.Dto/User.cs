// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using HttpMethod = Magicalizer.Api.Dto.Abstractions.HttpMethod;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/users")]
[AuthorizedOnly($"{nameof(User)}.{nameof(HttpMethod.Get)}", HttpMethod.Get)]
[AuthorizedOnly($"{nameof(User)}.{nameof(HttpMethod.Post)}", HttpMethod.Post)]
[AuthorizedOnly($"{nameof(User)}.{nameof(HttpMethod.Put)}", HttpMethod.Put)]
[AuthorizedOnly($"{nameof(User)}.{nameof(HttpMethod.Patch)}", HttpMethod.Patch)]
[AuthorizedOnly($"{nameof(User)}.{nameof(HttpMethod.Patch)}", HttpMethod.Delete)]
public class User : IDto<Domain.Models.User>
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public DateTime Created { get; set; }
  public IEnumerable<Credential>? Credentials { get; set; }
  public IEnumerable<UserRole>? UserRoles { get; set; }

  public User() { }

  public User(Domain.Models.User _user)
  {
    this.Id = _user.Id;
    this.Name = _user.Name;
    this.Created = _user.Created;
    this.Credentials = _user.Credentials?.Select(c => new Credential(c)).ToList();
    this.UserRoles = _user.UserRoles?.Select(ur => new UserRole(ur)).ToList();
  }

  public Domain.Models.User ToModel()
  {
    return new Domain.Models.User()
    {
      Id = Id,
      Name = Name,
      Created = Created
    };
  }
}
