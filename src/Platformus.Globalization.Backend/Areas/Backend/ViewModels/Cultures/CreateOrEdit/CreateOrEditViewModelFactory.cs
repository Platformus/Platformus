// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Backend.ViewModels.Cultures
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
        };

      Culture culture = this.RequestHandler.Storage.GetRepository<ICultureRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = culture.Id,
        Code = culture.Code,
        Name = culture.Name,
        IsFrontendDefault = culture.IsFrontendDefault,
        IsBackendDefault = culture.IsBackendDefault
      };
    }
  }
}