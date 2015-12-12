// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Frontend.ViewComponents;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Forms.Frontend.ViewModels.Shared;
using Platformus.Globalization;

namespace Platformus.Forms.Frontend.ViewComponents
{
  public class FormViewComponent : ViewComponentBase
  {
    public FormViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public IViewComponentResult Invoke(string code)
    {
      CachedForm cachedForm = this.Storage.GetRepository<ICachedFormRepository>().WithCultureIdAndCode(
        CultureProvider.GetCulture(this.Storage).Id, code
      );

      if (cachedForm == null)
      {
        Form form = this.Storage.GetRepository<IFormRepository>().WithCode(code);

        if (form == null)
          return null;

        return this.View(new FormViewModelBuilder(this).Build(form));
      }

      return this.View(new FormViewModelBuilder(this).Build(cachedForm));
    }
  }
}