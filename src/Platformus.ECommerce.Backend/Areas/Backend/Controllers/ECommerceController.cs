// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.ECommerce;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  public class ECommerceController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public ECommerceController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult CategorySelectorForm(int? categoryId)
    {
      return this.PartialView("_CategorySelectorForm", new CategorySelectorFormViewModelFactory(this).Create(categoryId));
    }

    public ActionResult ProductSelectorForm(int? productId)
    {
      return this.PartialView("_ProductSelectorForm", new ProductSelectorFormViewModelFactory(this).Create(productId));
    }

    public ActionResult GetCategoryName(int? categoryId)
    {
      if (categoryId == null)
        return this.Content("<div class=\"category-product-provider-parameter-editor__name\">Not selected</div>");

      Category category = this.Storage.GetRepository<ICategoryRepository>().WithKey((int)categoryId);

      return this.Content(string.Format("<div class=\"category-product-provider-parameter-editor__name\">{0}</div>", this.GetLocalizationValue(category.NameId)));
    }
  }
}