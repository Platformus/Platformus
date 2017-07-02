// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class ClassViewModelFactory : ViewModelFactoryBase
  {
    public ClassViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ClassViewModel Create(Class @class)
    {
      Class parent = @class.ClassId == null ? null : this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)@class.ClassId);

      return new ClassViewModel()
      {
        Id = @class.Id,
        Parent = parent == null ? null : new ClassViewModelFactory(this.RequestHandler).Create(parent),
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsAbstract = @class.IsAbstract
      };
    }
  }
}