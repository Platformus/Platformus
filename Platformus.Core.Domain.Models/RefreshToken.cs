// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class RefreshToken : IModel<Data.Entities.RefreshToken, RefreshTokenFilter>
{
  public string? Id { get; set; }
  public User? User { get; set; }
  public DateTime Created { get; set; }
  public DateTime? Used { get; set; }

  public RefreshToken() { }

  public RefreshToken(Data.Entities.RefreshToken _refreshToken)
  {
    this.Id = _refreshToken.Id;
    this.User = _refreshToken.User == null ? new User() { Id = _refreshToken.UserId } : new User(_refreshToken.User);
    this.Created = _refreshToken.Created;
    this.Used = _refreshToken.Used;
  }

  public Data.Entities.RefreshToken ToEntity()
  {
    return new Data.Entities.RefreshToken
    {
      Id = this.Id,
      UserId = this.User?.Id ?? 0,
      Created = this.Created,
      Used = this.Used
    };
  }
}