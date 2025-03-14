// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Localization;

namespace Platformus.Core.Admin;

public class Metadata : IMetadata
{
  private readonly IStringLocalizer<Resources.Core> stringLocalizer;

  public Metadata(IStringLocalizer<Resources.Core> stringLocalizer)
  {
    this.stringLocalizer = stringLocalizer;
  }

  public async Task<IEnumerable<Link>> GetLinksAsync() =>
    [
      new Link { Url = "/users", Label = this.stringLocalizer["Users"], Position = 100 },
      new Link { Url = "/roles", Label = this.stringLocalizer["Roles"], Position = 200 },
      new Link { Url = "/cultures", Label = this.stringLocalizer["Cultures"], Position = 300 },
      new Link { Url = "/configurations", Label = this.stringLocalizer["Configurations"], Position = 400 },
    ];
}