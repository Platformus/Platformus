// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Features
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Feature Map(CreateOrEditViewModel createOrEdit)
    {
      Feature feature = new Feature();

      if (createOrEdit.Id != null)
        feature = this.RequestHandler.Storage.GetRepository<IFeatureRepository>().WithKey((int)createOrEdit.Id);

      feature.Code = createOrEdit.Code;
      feature.Position = createOrEdit.Position;
      return feature;
    }
  }
}