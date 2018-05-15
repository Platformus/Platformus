// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Attributes
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
          ValueLocalizations = this.GetLocalizations()
        };

      Attribute attribute = this.RequestHandler.Storage.GetRepository<IAttributeRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = attribute.Id,
        ValueLocalizations = this.GetLocalizations(attribute.ValueId),
        Position = attribute.Position
      };
    }
  }
}