// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Frontend.ViewComponents;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
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

    public async Task<IViewComponentResult> InvokeAsync(string code, string partialViewName = null, string additionalCssClass = null)
    {
      SerializedForm serializedForm = this.Storage.GetRepository<ISerializedFormRepository>().WithCultureIdAndCode(
        this.GetService<ICultureManager>().GetCurrentCulture().Id, code
      );

      if (serializedForm == null)
        return this.Content($"There is no form with code “{code}” defined.");

      return this.GetService<ICache>().GetFormViewComponentResultWithDefaultValue(
        code, additionalCssClass, () => this.GetViewComponentResult(code, partialViewName, additionalCssClass)
      );
    }

    private IViewComponentResult GetViewComponentResult(string code, string partialViewName = null, string additionalCssClass = null)
    {
      SerializedForm serializedForm = this.Storage.GetRepository<ISerializedFormRepository>().WithCultureIdAndCode(
        this.GetService<ICultureManager>().GetCurrentCulture().Id, code
      );

      if (serializedForm == null)
        return this.Content($"There is no form with code “{code}” defined.");

      return this.View(new FormViewModelFactory(this).Create(serializedForm, partialViewName, additionalCssClass));
    }
  }
}