// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.FieldOptions
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

      FieldOption fieldOption = this.RequestHandler.Storage.GetRepository<IFieldOptionRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = fieldOption.Id,
        ValueLocalizations = this.GetLocalizations(fieldOption.ValueId),
        Position = fieldOption.Position
      };
    }
  }
}