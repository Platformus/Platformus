// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class PropertyViewModelFactory : ViewModelFactoryBase
  {
    public PropertyViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PropertyViewModel Create(Property property)
    {
      return new PropertyViewModel()
      {
        Id = property.Id,
        HtmlLocalizations = this.GetLocalizations(
          this.RequestHandler.Storage.GetRepository<IDictionaryRepository>().WithKey(property.HtmlId)
        )
      };
    }
  }
}