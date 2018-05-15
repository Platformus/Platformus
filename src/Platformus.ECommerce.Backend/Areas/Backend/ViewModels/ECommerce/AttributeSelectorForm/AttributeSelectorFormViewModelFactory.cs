// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public class AttributeSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public AttributeSelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public AttributeSelectorFormViewModel Create(int? attributeId)
    {
      IStringLocalizer<AttributeSelectorFormViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<AttributeSelectorFormViewModelFactory>>();

      return new AttributeSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Feature"]),
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Value"])
        },
        Attributes = this.RequestHandler.Storage.GetRepository<IAttributeRepository>().All().Select(
          a => new AttributeViewModelFactory(this.RequestHandler).Create(a, true)
        ),
        AttributeId = attributeId
      };
    }
  }
}