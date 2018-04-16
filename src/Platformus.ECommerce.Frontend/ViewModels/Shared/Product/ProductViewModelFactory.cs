// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class ProductViewModelFactory : ViewModelFactoryBase
  {
    public ProductViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ProductViewModel Create(Product product)
    {
      Photo photo = this.RequestHandler.Storage.GetRepository<IPhotoRepository>().CoverByProductId(product.Id);

      return new ProductViewModel()
      {
        Id = product.Id,
        Name = this.RequestHandler.GetLocalizationValue(product.NameId),
        Price = product.Price,
        Photo = new PhotoViewModelFactory(this.RequestHandler).Create(photo)
      };
    }
  }
}