// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Frontend.ViewComponents;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewComponents
{
  public class CatalogsViewComponent : ViewComponentBase
  {
    private ICache cache;

    public CatalogsViewComponent(IStorage storage, ICache cache)
      : base(storage)
    {
      this.cache = cache;
    }

    public async Task<IViewComponentResult> InvokeAsync(string partialViewName = null, string additionalCssClass = null)
    {
      IEnumerable<Catalog> catalogs = await this.cache.GetCatalogsWithDefaultValue(
        async () => await this.Storage.GetRepository<int, Catalog, CatalogFilter>().GetAllAsync(
          new CatalogFilter() { Owner = new CatalogFilter() { Id = new IntegerFilter(isNull: true) } },
          inclusions: new Inclusion<Catalog>[] {
            new Inclusion<Catalog>("Name.Localizations"),
            new Inclusion<Catalog>("Catalogs.Name.Localizations"),
            new Inclusion<Catalog>("Catalogs.Catalogs.Name.Localizations") }
        )
      );

      return this.View(new CatalogsViewModelFactory().Create(this.HttpContext, catalogs, partialViewName, additionalCssClass));
    }
  }
}