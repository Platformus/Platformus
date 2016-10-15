// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Content.Frontend.ViewModels.Shared;
using Platformus.Globalization;

namespace Platformus.Content.Frontend.Controllers
{
  [AllowAnonymous]
  public class DefaultController : Barebone.Frontend.Controllers.ControllerBase
  {
    public DefaultController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string url)
    {
      url = string.Format("/{0}", url);

      CachedObject cachedObject = this.Storage.GetRepository<ICachedObjectRepository>().WithCultureIdAndUrl(
        CultureManager.GetCurrentCulture(this.Storage).Id, url
      );

      if (cachedObject == null)
      {
        Object @object = this.Storage.GetRepository<IObjectRepository>().WithUrl(url);

        if (@object == null)
          return this.NotFound();

        ObjectViewModel result = new ObjectViewModelFactory(this).Create(@object);

        return this.View(result.ViewName, result);
      }

      {
        ObjectViewModel result = new ObjectViewModelFactory(this).Create(cachedObject);

        return this.View(result.ViewName, result);
      }
    }
  }
}