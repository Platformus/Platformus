// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Product Map(CreateOrEditViewModel createOrEdit)
    {
      Product product = new Product();

      if (createOrEdit.Id != null)
        product = this.RequestHandler.Storage.GetRepository<IProductRepository>().WithKey((int)createOrEdit.Id);

      product.CategoryId = createOrEdit.CategoryId;
      product.Url = createOrEdit.Url;
      product.Code = createOrEdit.Code;
      product.Price = createOrEdit.Price;
      return product;
    }
  }
}