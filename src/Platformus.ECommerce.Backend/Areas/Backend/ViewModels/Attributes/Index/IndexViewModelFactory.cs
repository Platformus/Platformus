// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Attributes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int featureId, string orderBy, string direction, int skip, int take, string filter)
    {
      IAttributeRepository attributeRepository = this.RequestHandler.Storage.GetRepository<IAttributeRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        FeatureId = featureId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, attributeRepository.Count(featureId, filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Value"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          attributeRepository.Range(featureId, orderBy, direction, skip, take, filter).ToList().Select(a => new AttributeViewModelFactory(this.RequestHandler).Create(a)),
          "_Attribute"
        )
      };
    }
  }
}