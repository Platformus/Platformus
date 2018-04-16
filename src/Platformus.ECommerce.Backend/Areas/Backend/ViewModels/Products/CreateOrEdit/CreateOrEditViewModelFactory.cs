// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          CategoryOptions = this.GetCategoryOptions(),
          Url = "/",
          NameLocalizations = this.GetLocalizations(),
          DescriptionLocalizations = this.GetLocalizations(),
          TitleLocalizations = this.GetLocalizations(),
          MetaDescriptionLocalizations = this.GetLocalizations(),
          MetaKeywordsLocalizations = this.GetLocalizations()
        };

      Product product = this.RequestHandler.Storage.GetRepository<IProductRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = product.Id,
        CategoryId = product.CategoryId,
        CategoryOptions = this.GetCategoryOptions(),
        Url = product.Url,
        Code = product.Code,
        NameLocalizations = this.GetLocalizations(product.NameId),
        DescriptionLocalizations = this.GetLocalizations(product.DescriptionId),
        Price = product.Price,
        TitleLocalizations = this.GetLocalizations(product.TitleId),
        MetaDescriptionLocalizations = this.GetLocalizations(product.MetaDescriptionId),
        MetaKeywordsLocalizations = this.GetLocalizations(product.MetaKeywordsId),
        Photos = this.GetPhotos(product)
      };
    }

    private IEnumerable<Option> GetCategoryOptions()
    {
      return this.RequestHandler.Storage.GetRepository<ICategoryRepository>().FilteredByCategoryId(null).ToList().Select(
        c => new Option(this.GetLocalizationValue(c.NameId), c.Id.ToString())
      );
    }

    private IEnumerable<PhotoViewModel> GetPhotos(Product product)
    {
      return this.RequestHandler.Storage.GetRepository<IPhotoRepository>().FilteredByProductId(product.Id).ToList().Select(
        ph => new PhotoViewModelFactory(this.RequestHandler).Create(ph)
      );
    }
  }
}