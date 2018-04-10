// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create()
    {
      return new IndexViewModel()
      {
        Categories = this.RequestHandler.Storage.GetRepository<ICategoryRepository>().FilteredByCategoryId(null).ToList().Select(
          c => new CategoryViewModelFactory(this.RequestHandler).Create(c)
        )
      };
    }
  }
}