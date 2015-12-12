// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
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
      return new ClassViewModel()
      {
        Id = @class.Id,
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsStandalone = @class.IsStandalone == true
      };
    }
  }
}