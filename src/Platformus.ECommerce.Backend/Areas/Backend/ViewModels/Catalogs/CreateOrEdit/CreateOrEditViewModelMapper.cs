// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Catalogs
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Catalog Map(CreateOrEditViewModel createOrEdit)
    {
      Catalog catalog = new Catalog();

      if (createOrEdit.Id != null)
        catalog = this.RequestHandler.Storage.GetRepository<ICatalogRepository>().WithKey((int)createOrEdit.Id);

      else catalog.CatalogId = createOrEdit.CatalogId;

      catalog.Url = createOrEdit.Url;
      catalog.CSharpClassName = createOrEdit.CSharpClassName;
      catalog.Parameters = createOrEdit.Parameters;
      catalog.Position = createOrEdit.Position;
      return catalog;
    }
  }
}