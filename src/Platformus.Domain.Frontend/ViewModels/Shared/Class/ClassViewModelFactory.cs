// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Domain.Frontend.ViewModels.Shared
{
  public class ClassViewModelFactory : ViewModelFactoryBase
  {
    public ClassViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ClassViewModel Create(int id, string viewName)
    {
      return new ClassViewModel()
      {
        Id = id,
        ViewName = viewName
      };
    }
  }
}