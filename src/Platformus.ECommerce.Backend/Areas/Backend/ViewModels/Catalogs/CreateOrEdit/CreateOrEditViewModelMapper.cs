// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Catalogs
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Catalog Map(CatalogFilter filter, Catalog catalog, CreateOrEditViewModel createOrEdit)
    {
      if (catalog.Id == 0)
        catalog.CatalogId = filter?.Owner?.Id?.Equals;

      catalog.Url = createOrEdit.Url;
      catalog.CSharpClassName = createOrEdit.CSharpClassName;
      catalog.Parameters = createOrEdit.Parameters;
      catalog.Position = createOrEdit.Position;
      return catalog;
    }
  }
}