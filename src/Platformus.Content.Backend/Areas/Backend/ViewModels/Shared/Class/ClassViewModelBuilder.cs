// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class ClassViewModelBuilder : ViewModelBuilderBase
  {
    public ClassViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public ClassViewModel Build(Class @class)
    {
      Class parent = @class.ClassId == null ? null : this.handler.Storage.GetRepository<IClassRepository>().WithKey((int)@class.ClassId);

      return new ClassViewModel()
      {
        Id = @class.Id,
        Parent = parent == null ? null : new ClassViewModelBuilder(this.handler).Build(parent),
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsAbstract = @class.IsAbstract == true,
        IsStandalone = @class.IsStandalone == true
      };
    }
  }
}