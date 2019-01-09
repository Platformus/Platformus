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

    public ActionResult AttributeSelectorForm(int? attributeId)
    {
      return this.PartialView("_AttributeSelectorForm", new AttributeSelectorFormViewModelFactory(this).Create(attributeId));
    }

    public ActionResult GetCategoryName(int? categoryId)
    {
      if (categoryId == null)
        return this.Content("<div class=\"item-selector__key\">Not selected</div>");

      Category category = this.Storage.GetRepository<ICategoryRepository>().WithKey((int)categoryId);

      return this.Content(string.Format("<div class=\"item-selector__key\">{0}</div>", this.GetLocalizationValue(category.NameId)));
    }

    public ActionResult GetAttribute(int attributeId)
    {
      Attribute attribute = this.Storage.GetRepository<IAttributeRepository>().WithKey(attributeId);
      Feature feature = this.Storage.GetRepository<IFeatureRepository>().WithKey(attribute.FeatureId);

      return this.Json(new { id = attribute.Id, feature = new { id = feature.Id, name = this.GetLocalizationValue(feature.NameId) }, value = this.GetLocalizationValue(attribute.ValueId) });
    }
  }
}