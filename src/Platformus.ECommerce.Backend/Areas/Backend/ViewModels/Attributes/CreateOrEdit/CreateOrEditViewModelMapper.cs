// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Attributes
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Attribute Map(CreateOrEditViewModel createOrEdit)
    {
      Attribute attribute = new Attribute();

      if (createOrEdit.Id != null)
        attribute = this.RequestHandler.Storage.GetRepository<IAttributeRepository>().WithKey((int)createOrEdit.Id);

      else attribute.FeatureId = createOrEdit.FeatureId;

      attribute.Position = createOrEdit.Position;
      return attribute;
    }
  }
}