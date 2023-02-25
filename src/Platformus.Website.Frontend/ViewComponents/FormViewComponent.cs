// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;
using Platformus.Website.Frontend.ViewModels.Shared;

namespace Platformus.Website.Frontend.ViewComponents;

public class FormViewComponent : ViewComponent
{
  private IStorage storage;
  private ICache cache;

  public FormViewComponent(IStorage storage, ICache cache)
  {
    this.storage = storage;
    this.cache = cache;
  }

  public async Task<IViewComponentResult> InvokeAsync(string code, string partialViewName = "_Form", string additionalCssClass = null)
  {
    Form form = await this.cache.GetFormWithDefaultValue(
      code,
      async () => (await this.storage.GetRepository<int, Form, FormFilter>().GetAllAsync(
        new FormFilter(code: code),
        inclusions: new Inclusion<Form>[] {
          new Inclusion<Form>(f => f.Name.Localizations),
          new Inclusion<Form>(f => f.SubmitButtonTitle.Localizations),
          new Inclusion<Form>("Fields.FieldType"),
          new Inclusion<Form>("Fields.Name.Localizations"),
          new Inclusion<Form>("Fields.FieldOptions.Value.Localizations") }
      )).FirstOrDefault()
    );

    if (form == null)
      return this.Content($"There is no form with code “{code}” defined.");

    return this.View(FormViewModelFactory.Create(form, partialViewName, additionalCssClass));
  }
}