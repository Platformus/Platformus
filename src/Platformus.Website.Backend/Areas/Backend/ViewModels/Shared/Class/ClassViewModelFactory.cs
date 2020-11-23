// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class ClassViewModelFactory : ViewModelFactoryBase
  {
    public ClassViewModel Create(Class @class)
    {
      return new ClassViewModel()
      {
        Id = @class.Id,
        Parent = @class.Parent == null ? null : new ClassViewModelFactory().Create(@class.Parent),
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsAbstract = @class.IsAbstract
      };
    }
  }
}