// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Localization;
using Platformus.Core.Admin;
using Platformus.Website.Admin.Services;
using Platformus.Website.Api.Dto;

namespace Platformus.Website.Admin;

public class Metadata : IMetadata
{
  private readonly ClassCache classCache;
  private readonly IStringLocalizer<Resources.Website> stringLocalizer;

  public Metadata(ClassCache classCache, IStringLocalizer<Resources.Website> stringLocalizer)
  {
    this.classCache = classCache;
    this.stringLocalizer = stringLocalizer;
  }

  public async Task<IEnumerable<Link>> GetLinksAsync()
  {
    IEnumerable<Class> classes = await this.classCache.GetClassesAsync();

    return [
      new Link {
        Url = "/objects",
        Label = this.stringLocalizer["Objects"],
        Position = 1000,
        Links = classes.Select(
          c => new Link {
            Url = $"/objects/{c.UrlSegment}",
            Label = c.PluralName
          }
        ).ToList()
      },
      new Link { Url = "/menus", Label = this.stringLocalizer["Menus"], Position = 1010 },
      new Link { Url = "/forms", Label = this.stringLocalizer["Forms"], Position = 1020 },
      new Link { Url = "/classes", Label = this.stringLocalizer["Classes"], Position = 1030 },
    ];
  }
}
