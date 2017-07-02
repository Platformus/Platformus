// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Backend.ViewModels.Shared
{
  public class CultureViewModelFactory : ViewModelFactoryBase
  {
    public CultureViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CultureViewModel Create(Culture culture)
    {
      return new CultureViewModel()
      {
        Id = culture.Id,
        Code = culture.Code,
        Name = culture.Name
      };
    }
  }
}