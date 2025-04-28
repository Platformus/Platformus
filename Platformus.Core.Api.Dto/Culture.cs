// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using HttpMethod = Magicalizer.Api.Dto.Abstractions.HttpMethod;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/cultures")]
[AuthorizedOnly($"{nameof(Culture)}.{nameof(HttpMethod.Get)}", HttpMethod.Get)]
[AuthorizedOnly($"{nameof(Culture)}.{nameof(HttpMethod.Post)}", HttpMethod.Post)]
[AuthorizedOnly($"{nameof(Culture)}.{nameof(HttpMethod.Put)}", HttpMethod.Put)]
[AuthorizedOnly($"{nameof(Culture)}.{nameof(HttpMethod.Patch)}", HttpMethod.Patch)]
[AuthorizedOnly($"{nameof(Culture)}.{nameof(HttpMethod.Patch)}", HttpMethod.Delete)]
public class Culture : IDto<Domain.Models.Culture>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public Culture() { }

  public Culture(Domain.Models.Culture _culture)
  {
    this.Id = _culture.Id;
    this.Name = _culture.Name;
  }

  public Domain.Models.Culture ToModel()
  {
    return new Domain.Models.Culture()
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}