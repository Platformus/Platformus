// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public static class CreateOrEditViewModelMapper
  {
    public static Product Map(Product product, CreateOrEditViewModel createOrEdit)
    {
      product.CategoryId = createOrEdit.CategoryId;
      product.Url = createOrEdit.Url;
      product.Code = createOrEdit.Code;
      product.Price = createOrEdit.Price;
      return product;
    }
  }
}